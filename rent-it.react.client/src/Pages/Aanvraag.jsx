import React, { useState, useEffect } from "react";
import Navbar from "/src/Components/Navbar";
import Footer from "/src/Components/Footer";
import "/src/css/Aanvraag.css";

const Aanvraag = () => {
    // Bestaande state
    const [voertuigType, setVoertuigType] = useState("Auto's");
    const [merk, setMerk] = useState("");
    const [maxPrijs, setMaxPrijs] = useState("");
    const [startDatum, setStartDatum] = useState("");
    const [eindDatum, setEindDatum] = useState("");
    const [verwachteKilometers, setVerwachteKilometers] = useState("");
    const [aardeVanReis, setAardeVanReis] = useState("");
    const [versteBestemming, setVersteBestemming] = useState("");
    const [rijbewijsDocNr, setRijbewijsDocNr] = useState("");
    const [beschikbareVoertuigen, setBeschikbareVoertuigen] = useState([]);
    const [geselecteerdVoertuig, setGeselecteerdVoertuig] = useState(null);
    const [status, setStatus] = useState("");
    const [sortOption, setSortOption] = useState("");
    const [loading, setLoading] = useState(false);

    // Nieuwe state voor uitgebreide functionaliteit
    const [voorwaardenGeaccepteerd, setVoorwaardenGeaccepteerd] = useState(false);
    const [prijsDetails, setPrijsDetails] = useState({
        basisHuur: 0,
        verzekering: 0,
        borg: 0,
        totaal: 0
    });

    useEffect(() => {
        if (geselecteerdVoertuig && startDatum && eindDatum) {
            berekenGedetailleerdePrijs();
        }
    }, [geselecteerdVoertuig, startDatum, eindDatum, verwachteKilometers]);

    const berekenGedetailleerdePrijs = () => {
        if (!geselecteerdVoertuig || !startDatum || !eindDatum) return;

        const dagen = Math.ceil(
            (new Date(eindDatum) - new Date(startDatum)) / (1000 * 60 * 60 * 24)
        );

        const basisHuurPrijs = dagen * geselecteerdVoertuig.prijsPerDag;
        const verzekeringKosten = dagen * (geselecteerdVoertuig.verzekeringTarief || 15); // Standaard verzekeringstarief
        const borgBedrag = geselecteerdVoertuig.borgBedrag || 500; // Standaard borg
        const extraKmKosten = berekenExtraKilometerKosten();

        const nieuweDetails = {
            basisHuur: basisHuurPrijs,
            verzekering: verzekeringKosten,
            borg: borgBedrag,
            extraKmKosten: extraKmKosten,
            totaal: basisHuurPrijs + verzekeringKosten + extraKmKosten
        };

        setPrijsDetails(nieuweDetails);
    };

    const berekenExtraKilometerKosten = () => {
        if (!verwachteKilometers || !geselecteerdVoertuig) return 0;

        const kmPerDag = geselecteerdVoertuig.kmPerDag || 100; // Standaard km per dag
        const dagen = Math.ceil(
            (new Date(eindDatum) - new Date(startDatum)) / (1000 * 60 * 60 * 24)
        );
        const toegestaneKm = kmPerDag * dagen;
        const extraKm = Math.max(0, verwachteKilometers - toegestaneKm);
        return extraKm * (geselecteerdVoertuig.extraKmTarief || 0.20); // Standaard tarief per extra km
    };

    const validateAanvraag = () => {
        // Valideer datums
        if (!startDatum || !eindDatum) {
            setStatus("Selecteer een start- en einddatum");
            return false;
        }

        if (new Date(startDatum) < new Date()) {
            setStatus("Startdatum mag niet in het verleden liggen");
            return false;
        }

        if (new Date(startDatum) >= new Date(eindDatum)) {
            setStatus("Einddatum moet na startdatum liggen");
            return false;
        }

        // Valideer rijbewijs
        if (!rijbewijsDocNr || rijbewijsDocNr.length < 8) {
            setStatus("Vul een geldig rijbewijsnummer in");
            return false;
        }

        // Valideer kilometers
        if (!verwachteKilometers || verwachteKilometers < 1) {
            setStatus("Vul een geldig aantal kilometers in");
            return false;
        }

        // Valideer voorwaarden
        if (!voorwaardenGeaccepteerd) {
            setStatus("U dient akkoord te gaan met de voorwaarden");
            return false;
        }

        return true;
    };

    const fetchVoertuigen = async () => {
        if (!startDatum || !eindDatum || new Date(startDatum) > new Date(eindDatum)) {
            setStatus("Controleer de ingevoerde datums");
            return;
        }

        setLoading(true);
        try {
            const response = await fetch(
                `/api/VerhuurAanvraag/Voertuigen?type=${voertuigType}&merk=${merk}&maxPrijs=${maxPrijs}&startDatum=${startDatum}&eindDatum=${eindDatum}&sorteerOp=${sortOption}`
            );
            const data = await response.json();
            setBeschikbareVoertuigen(data);
            setStatus("");
        } catch (error) {
            console.error("Fout bij ophalen van voertuigen:", error);
            setStatus("Er is een fout opgetreden bij het ophalen van voertuigen.");
        } finally {
            setLoading(false);
        }
    };

    const handleAanvraag = async () => {
        if (!validateAanvraag()) {
            return;
        }

        try {
            const aanvraagData = {
                voertuigID: geselecteerdVoertuig.voertuigId,
                startDatum,
                eindDatum,
                rijbewijsDocNr,
                aardeVanReis,
                versteBestemming,
                verwachteKilometers: parseInt(verwachteKilometers),
                voorwaardenGeaccepteerd,
                prijsDetails,
                status: "In behandeling",
            };

            const response = await fetch("/api/VerhuurAanvraag/Aanvraag", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(aanvraagData),
            });

            if (response.ok) {
                setStatus("Aanvraag succesvol ingediend! U ontvangt een bevestigingsmail.");
                resetForm();
            } else {
                const errorData = await response.json();
                setStatus(errorData.message || "Er is een fout opgetreden.");
            }
        } catch (error) {
            console.error("Fout bij indienen aanvraag:", error);
            setStatus("Er is een fout opgetreden bij het indienen van uw aanvraag.");
        }
    };

    const resetForm = () => {
        setGeselecteerdVoertuig(null);
        setStartDatum("");
        setEindDatum("");
        setRijbewijsDocNr("");
        setAardeVanReis("");
        setVersteBestemming("");
        setVerwachteKilometers("");
        setVoorwaardenGeaccepteerd(false);
        setPrijsDetails({
            basisHuur: 0,
            verzekering: 0,
            borg: 0,
            totaal: 0
        });
    };

    return (
        <div className="mainWrapper">
            <Navbar />
            <div className="aanvraagWrapper">
                <h1>Reserveer je Voertuig</h1>

                {/* Zoekformulier */}
                <div className="formSection">
                    <div className="search-grid">
                        <label>
                            Type voertuig:
                            <select value={voertuigType} onChange={(e) => setVoertuigType(e.target.value)}>
                                <option value="Auto's">Autos</option>
                                <option value="Campers">Campers</option>
                                <option value="Caravans">Caravans</option>
                            </select>
                        </label>
                        <label>
                            Merk:
                            <input
                                type="text"
                                value={merk}
                                onChange={(e) => setMerk(e.target.value)}
                                placeholder="Bijv. Toyota"
                            />
                        </label>
                        <label>
                            Maximale prijs per dag:
                            <input
                                type="number"
                                value={maxPrijs}
                                onChange={(e) => setMaxPrijs(e.target.value)}
                                placeholder="€"
                            />
                        </label>
                        <label>
                            Startdatum:
                            <input
                                type="date"
                                value={startDatum}
                                min={new Date().toISOString().split('T')[0]}
                                onChange={(e) => setStartDatum(e.target.value)}
                            />
                        </label>
                        <label>
                            Einddatum:
                            <input
                                type="date"
                                value={eindDatum}
                                min={startDatum || new Date().toISOString().split('T')[0]}
                                onChange={(e) => setEindDatum(e.target.value)}
                            />
                        </label>
                        <label>
                            Sorteer op:
                            <select value={sortOption} onChange={(e) => setSortOption(e.target.value)}>
                                <option value="">Geen</option>
                                <option value="prijs">Prijs oplopend</option>
                                <option value="prijsdesc">Prijs aflopend</option>
                                <option value="merk">Merk</option>
                            </select>
                        </label>
                    </div>
                    <button className="searchButton" onClick={fetchVoertuigen}>
                        Zoek beschikbare voertuigen
                    </button>
                </div>

                {/* Beschikbare voertuigen lijst */}
                {loading ? (
                    <div className="loading-spinner">Voertuigen laden...</div>
                ) : (
                    <div className="voertuigenLijst">
                        <h2>Beschikbare Voertuigen</h2>
                        {beschikbareVoertuigen.length > 0 ? (
                            <ul>
                                {beschikbareVoertuigen.map((voertuig) => (
                                    <li
                                        key={voertuig.voertuigId}
                                        onClick={() => setGeselecteerdVoertuig(voertuig)}
                                        className={geselecteerdVoertuig?.voertuigId === voertuig.voertuigId ? 'selected' : ''}
                                    >
                                        <div className="vehicle-header">
                                            <h3 className="vehicle-title">
                                                {voertuig.merk} {voertuig.type}
                                                <span className="vehicle-price-tag">€{voertuig.prijsPerDag}/dag</span>
                                            </h3>
                                        </div>
                                        <div className="vehicle-details">
                                            <div className="detail-item">
                                                <span className="detail-label">Kleur</span>
                                                <span className="detail-value">{voertuig.kleur}</span>
                                            </div>
                                            <div className="detail-item">
                                                <span className="detail-label">Bouwjaar</span>
                                                <span className="detail-value">{voertuig.bouwjaar}</span>
                                            </div>
                                            <div className="detail-item">
                                                <span className="detail-label">Type</span>
                                                <span className="detail-value">{voertuigType}</span>
                                            </div>
                                            <div className="detail-item">
                                                <span className="detail-label">Status</span>
                                                <span className="detail-value status-available">Beschikbaar</span>
                                            </div>
                                        </div>
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <p className="no-results">Geen voertuigen beschikbaar.</p>
                        )}
                    </div>
                )}

                {/* Geselecteerd voertuig en reserveringsformulier */}
                {geselecteerdVoertuig && (
                    <div className="geselecteerdVoertuig">
                        <h3>Geselecteerd Voertuig</h3>
                        <div className="selected-vehicle-info">
                            <p className="selected-vehicle-title">
                                {geselecteerdVoertuig.merk} {geselecteerdVoertuig.type}
                            </p>

                            {/* Prijsspecificatie */}
                            <div className="price-details">
                                <h4>Kostenspecificatie</h4>
                                <div className="price-grid">
                                    <div className="price-item">
                                        <span>Huurprijs</span>
                                        <span>€{prijsDetails.basisHuur.toFixed(2)}</span>
                                    </div>
                                    <div className="price-item">
                                        <span>Verzekering</span>
                                        <span>€{prijsDetails.verzekering.toFixed(2)}</span>
                                    </div>
                                    {prijsDetails.extraKmKosten > 0 && (
                                        <div className="price-item">
                                            <span>Extra kilometers</span>
                                            <span>€{prijsDetails.extraKmKosten.toFixed(2)}</span>
                                        </div>
                                    )}
                                    <div className="price-item">
                                        <span>Borg</span>
                                        <span>€{prijsDetails.borg.toFixed(2)}</span>
                                    </div>
                                    <div className="price-item total">
                                        <span>Totaal</span>
                                        <span>€{prijsDetails.totaal.toFixed(2)}</span>
                                    </div>
                                </div>
                            </div>

                            {/* Voorwaarden en inclusief */}
                            <div className="extra-informatie">
                                <h4>Inclusief</h4>
                                <ul>
                                    <li>WA-verzekering</li>
                                    <li>Pechhulp</li>
                                    <li>{geselecteerdVoertuig.kmPerDag || 100} km per dag</li>
                                </ul>

                                <h4>Voorwaarden</h4>
                                <ul>
                                    <li>Minimumleeftijd: 21 jaar</li>
                                    <li>Rijbewijs minimaal 1 jaar</li>
                                    <li>Borg: €{prijsDetails.borg.toFixed(2)}</li>
                                </ul>
                            </div>

                            {/* Reserveringsgegevens formulier */}
                            <div className="booking-details">
                                <h4>Reserveringsgegevens</h4>
                                <div className="booking-form-grid">
                                    <div className="form-group">
                                        <label>Verwachte kilometers</label>
                                        <input
                                            type="number"
                                            value={verwachteKilometers}
                                            onChange={(e) => setVerwachteKilometers(e.target.value)}
                                            placeholder="Bijv. 500"
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label>Aard van de reis</label>
                                        <input
                                            type="text"
                                            value={aardeVanReis}
                                            onChange={(e) => setAardeVanReis(e.target.value)}
                                            placeholder="Bijv. Vakantie"
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label>Verste bestemming</label>
                                        <input
                                            type="text"
                                            value={versteBestemming}
                                            onChange={(e) => setVersteBestemming(e.target.value)}
                                            placeholder="Bijv. Parijs"
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label>Rijbewijs documentnummer</label>
                                        <input
                                            type="text"
                                            value={rijbewijsDocNr}
                                            onChange={(e) => setRijbewijsDocNr(e.target.value)}
                                            placeholder="Bijv. 123456789"
                                        />
                                    </div>
                                </div>

                                {/* Voorwaarden acceptatie */}
                                <div className="voorwaarden-acceptatie">
                                    <label className="checkbox-container">
                                        <input
                                            type="checkbox"
                                            checked={voorwaardenGeaccepteerd}
                                            onChange={(e) => setVoorwaardenGeaccepteerd(e.target.checked)}
                                        />
                                        <span>Ik ga akkoord met de huurvoorwaarden en algemene voorwaarden</span>
                                    </label>
                                </div>
                            </div>
                        </div>

                        {/* Bevestigingsknop */}
                        <button
                            className="submitButton"
                            onClick={handleAanvraag}
                            disabled={!voorwaardenGeaccepteerd}
                        >
                            Bevestig Reservering
                        </button>
                    </div>
                )}

                {/* Statusbericht */}
                {status && <p className={`statusMessage ${status.includes('succesvol') ? 'success' : 'error'}`}>{status}</p>}
            </div>
            <Footer />
        </div>
    );
};

export default Aanvraag;