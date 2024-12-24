import React, { useState, useEffect } from "react";
import Navbar from "/src/Components/Navbar";
import Footer from "/src/Components/Footer";
import "/src/css/Aanvraag.css";

const Aanvraag = () => {
    const [voertuigType, setVoertuigType] = useState("Auto's");
    const [merk, setMerk] = useState("");
    const [beschikbareMerken, setBeschikbareMerken] = useState([]);
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
    const [totaalPrijs, setTotaalPrijs] = useState(0);
    const [sortOption, setSortOption] = useState("");
    const [loading, setLoading] = useState(false);

    // Merken per type voertuig
    const merkenPerType = {
        "Auto's": [
            "Adria", "Audi", "BMW", "Citroen", "Fiat", "Ford", "Honda", "Hyundai", "Jeep", "Kia", "Land Rover", "Mazda", "Mercedes", "Mitsubishi", "Nissan", "Opel", "Peugeot", "Renault", "Skoda", "Subaru", "Suzuki", "Toyota", "Volkswagen", "Volvo"
        ],
        Caravans: [
            "Adria", "Bailey", "Buccaneer", "Burstner", "Caravelair", "Coachman", "Compass", "Dethleffs", "Eriba", "Fendt", "Hobby", "Knaus", "LMC", "Lunar", "Sprite", "Sterckeman", "Swift", "Tab", "Tabbert"
        ],
        Campers: [
            "Citroen", "Fiat", "Ford", "Iveco", "Mercedes", "Nissan", "Opel", "Peugeot", "Renault", "Volkswagen"
        ]
    };

    // Update beschikbare merken bij veranderen voertuigtype
    useEffect(() => {
        setBeschikbareMerken(merkenPerType[voertuigType] || []);
        setMerk(""); // Reset geselecteerd merk
    }, [voertuigType]);

    // Bereken totaalprijs
    useEffect(() => {
        if (geselecteerdVoertuig && startDatum && eindDatum) {
            const dagen = Math.ceil(
                (new Date(eindDatum) - new Date(startDatum)) / (1000 * 60 * 60 * 24)
            );
            setTotaalPrijs(dagen * geselecteerdVoertuig.prijsPerDag);
        }
    }, [geselecteerdVoertuig, startDatum, eindDatum]);

    // Functie om voertuigen op te halen
    const fetchVoertuigen = async () => {
        if (!startDatum || !eindDatum || new Date(startDatum) > new Date(eindDatum)) {
            alert("Controleer de ingevoerde datums. De startdatum moet eerder zijn dan de einddatum.");
            return;
        }

        setLoading(true);
        try {
            const response = await fetch(
                `/api/VerhuurAanvraag/Voertuigen?type=${voertuigType}&merk=${merk}&maxPrijs=${maxPrijs}&startDatum=${startDatum}&eindDatum=${eindDatum}&sorteerOp=${sortOption}`
            );
            const data = await response.json();
            setBeschikbareVoertuigen(data);
        } catch (error) {
            console.error("Fout bij ophalen van voertuigen:", error);
        } finally {
            setLoading(false);
        }
    };

    // Functie om een aanvraag te versturen
    const handleAanvraag = async () => {
        if (
            !geselecteerdVoertuig ||
            !startDatum ||
            !eindDatum ||
            !rijbewijsDocNr ||
            !aardeVanReis ||
            !versteBestemming ||
            !verwachteKilometers ||
            verwachteKilometers <= 0
        ) {
            alert("Vul alle velden correct in voordat u een aanvraag indient.");
            return;
        }

        const aanvraagData = {
            voertuigID: geselecteerdVoertuig.voertuigId,
            startDatum,
            eindDatum,
            rijbewijsDocNr,
            aardeVanReis,
            versteBestemming,
            verwachteKilometers: parseInt(verwachteKilometers),
            status: "In behandeling",
        };

        try {
            const response = await fetch("/api/VerhuurAanvraag/Aanvraag", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(aanvraagData),
            });

            if (response.ok) {
                setStatus("Aanvraag succesvol ingediend!");
                setGeselecteerdVoertuig(null);
                setStartDatum("");
                setEindDatum("");
                setRijbewijsDocNr("");
                setAardeVanReis("");
                setVersteBestemming("");
                setVerwachteKilometers("");
            } else {
                const errorData = await response.json();
                setStatus(errorData.message || "Er is een fout opgetreden.");
            }
        } catch (error) {
            console.error("Fout bij indienen aanvraag:", error);
            setStatus("Er is een fout opgetreden bij het indienen van uw aanvraag.");
        }
    };

    return (
        <div className="mainWrapper">
            <Navbar />
            <div className="aanvraagWrapper">
                <h1>Reserveer je Voertuig</h1>
                <div className="formSection">
                    <label>
                        Type voertuig:
                        <select value={voertuigType} onChange={(e) => setVoertuigType(e.target.value)}>
                            <option value="Auto's">Auto's</option>
                            <option value="Campers">Campers</option>
                            <option value="Caravans">Caravans</option>
                        </select>
                    </label>
                    <label>
                        Merk (optioneel):
                        <select
                            value={merk}
                            onChange={(e) => setMerk(e.target.value)}
                            disabled={beschikbareMerken.length === 0}
                        >
                            <option value="">Selecteer een merk</option>
                            {beschikbareMerken.map((m, index) => (
                                <option key={index} value={m}>
                                    {m}
                                </option>
                            ))}
                        </select>
                    </label>
                    <label>
                        Maximale prijs (optioneel):
                        <input
                            type="number"
                            value={maxPrijs}
                            onChange={(e) => setMaxPrijs(e.target.value)}
                            placeholder="Bijv. 100"
                        />
                    </label>
                    <label>
                        Startdatum:
                        <input type="date" value={startDatum} onChange={(e) => setStartDatum(e.target.value)} />
                    </label>
                    <label>
                        Einddatum:
                        <input type="date" value={eindDatum} onChange={(e) => setEindDatum(e.target.value)} />
                    </label>
                    <label>
                        Sorteer op:
                        <select value={sortOption} onChange={(e) => setSortOption(e.target.value)}>
                            <option value="">Geen</option>
                            <option value="prijs">Prijs</option>
                            <option value="merk">Merk</option>
                        </select>
                    </label>
                    <button className="fetchButton" onClick={fetchVoertuigen}>
                        Zoek beschikbare voertuigen
                    </button>
                </div>

                {loading ? (
                    <p>Voertuigen laden...</p>
                ) : (
                    <div className="voertuigenLijst">
                        <h2>Beschikbare Voertuigen</h2>
                        {beschikbareVoertuigen.length > 0 ? (
                            <ul>
                                {beschikbareVoertuigen.map((voertuig) => (
                                    <li key={voertuig.voertuigId} onClick={() => setGeselecteerdVoertuig(voertuig)}>
                                        {voertuig.merk} {voertuig.type} - €{voertuig.prijsPerDag} per dag
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <p>Geen voertuigen beschikbaar.</p>
                        )}
                    </div>
                )}

                {geselecteerdVoertuig && (
                    <div className="geselecteerdVoertuig">
                        <h3>Geselecteerd Voertuig:</h3>
                        <p>
                            {geselecteerdVoertuig.merk} {geselecteerdVoertuig.type} - €{geselecteerdVoertuig.prijsPerDag} per dag
                        </p>
                        <p>Totaalprijs: €{totaalPrijs}</p>
                        <label>
                            Verwachte kilometers:
                            <input
                                type="number"
                                value={verwachteKilometers}
                                onChange={(e) => setVerwachteKilometers(e.target.value)}
                                placeholder="Bijv. 500"
                            />
                        </label>
                        <label>
                            Aarde van de reis:
                            <input
                                type="text"
                                value={aardeVanReis}
                                onChange={(e) => setAardeVanReis(e.target.value)}
                                placeholder="Bijv. Vakantie"
                            />
                        </label>
                        <label>
                            Verste bestemming:
                            <input
                                type="text"
                                value={versteBestemming}
                                onChange={(e) => setVersteBestemming(e.target.value)}
                                placeholder="Bijv. Parijs"
                            />
                        </label>
                        <label>
                            Rijbewijs documentnummer:
                            <input
                                type="text"
                                value={rijbewijsDocNr}
                                onChange={(e) => setRijbewijsDocNr(e.target.value)}
                                placeholder="Bijv. 123456789"
                            />
                        </label>
                    </div>
                )}

                <button className="submitButton" onClick={handleAanvraag}>
                    Bevestig Reservering
                </button>
                {status && <p className="statusMessage">{status}</p>}
            </div>
            <Footer />
        </div>
    );
};

export default Aanvraag;
