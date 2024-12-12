import React from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';

const PrivacyOvereenkomst = () => {
    return (
        <div className="mainWrapper">
            <Navbar />

            <main className="mainSection">
                <div className="textSection2">
                    <h3>Privacybeleid Rent-It</h3>
                    <p>
                        Welkom bij Rent-It. We hechten veel waarde aan uw privacy en de bescherming van uw persoonsgegevens.
                        Dit privacybeleid legt uit welke gegevens we verzamelen, hoe we deze gebruiken en welke rechten u heeft met betrekking tot uw gegevens.
                    </p>

                    <h4>1. Verzamelde gegevens</h4>
                    <ul>
                        <li>Identificatiegegevens: Naam, e-mailadres, telefoonnummer.</li>
                        <li>Accountgegevens: Gebruikersnaam, wachtwoord, voorkeuren.</li>
                        <li>Zakelijke gegevens: Bedrijfsnaam, KVK-nummer, adres.</li>
                        <li>Huur- en transactiegegevens: Huurgeschiedenis, betalingsinformatie.</li>
                        <li>Technische gegevens: IP-adres, browserinformatie, inloggegevens.</li>
                    </ul>

                    <h4>2. Doeleinden van gegevensverwerking</h4>
                    <p>Wij gebruiken uw gegevens uitsluitend voor:</p>
                    <ul>
                        <li>Het aanmaken en beheren van gebruikersaccounts.</li>
                        <li>Het verwerken van huuraanvragen en betalingen.</li>
                        <li>Klantcommunicatie, zoals notificaties en bevestigingen.</li>
                        <li>Beveiliging en fraudepreventie.</li>
                        <li>Naleving van wettelijke verplichtingen, zoals belastingwetgeving.</li>
                    </ul>

                    <h4>3. Rechtsgrondslag voor verwerking</h4>
                    <ul>
                        <li>Toestemming (voor specifieke verwerkingen zoals marketing).</li>
                        <li>Contractuele verplichtingen (bijvoorbeeld voor het verwerken van huuraanvragen).</li>
                        <li>Wettelijke verplichtingen (zoals fiscale regelgeving).</li>
                        <li>Gerechtvaardigde belangen (zoals beveiliging en fraudepreventie).</li>
                    </ul>

                    <h4>4. Bewaartermijnen</h4>
                    <ul>
                        <li>Persoonlijke gegevens: Maximaal 30 dagen na beeindiging van uw account.</li>
                        <li>Financiele gegevens: 7 jaar volgens de fiscale bewaarplicht.</li>
                        <li>Logs: 6 maanden, tenzij noodzakelijk voor fraudeonderzoek.</li>
                    </ul>

                    <h4>5. Uw rechten</h4>
                    <p>U heeft de volgende rechten:</p>
                    <ul>
                        <li>Inzage: U kunt een kopie opvragen van de gegevens die wij over u bewaren.</li>
                        <li>Correctie: U kunt foutieve gegevens laten aanpassen.</li>
                        <li>Verwijdering: U kunt verzoeken om verwijdering van uw gegevens, tenzij dit in strijd is met wettelijke verplichtingen.</li>
                        <li>Beperking: U kunt verzoeken om beperking van de gegevensverwerking.</li>
                        <li>Overdraagbaarheid: U kunt een kopie van uw gegevens ontvangen in een gestructureerd, gangbaar formaat.</li>
                    </ul>

                    <h4>6. Delen van gegevens</h4>
                    <ul>
                        <li>Met derden die ons ondersteunen bij dienstverlening, zoals betalingsproviders.</li>
                        <li>Indien wettelijk vereist, bijvoorbeeld met belastingautoriteiten.</li>
                        <li>Na uw expliciete toestemming.</li>
                    </ul>

                    <h4>7. Beveiliging</h4>
                    <p>
                        We nemen passende technische en organisatorische maatregelen om uw gegevens te beschermen tegen
                        ongeautoriseerde toegang, verlies of diefstal, zoals:
                    </p>
                    <ul>
                        <li>Encryptie van gevoelige gegevens.</li>
                        <li>Beperkte toegang tot systemen op basis van rollen.</li>
                        <li>Real-time monitoring van verdachte activiteiten.</li>
                    </ul>

                    <h4>8. Cookies</h4>
                    <p>
                        Wij gebruiken cookies om de gebruikerservaring te verbeteren. Voor meer informatie kunt u ons cookiebeleid raadplegen.
                    </p>

                    <h4>9. Contact</h4>
                    <p>
                        Heeft u vragen over dit beleid? Neem contact met ons op via:
                    </p>
                    <ul>
                        <li>E-mailadres: <a href="mailto:informatie.rentit@gmail.com">informatie.rentit@gmail.com</a></li>
                        <li>Postadres: Johanna Westerdijkplein 75, 2521 EP Den Haag</li>
                    </ul>
                </div>
            </main>

            <Footer />
        </div>
    );
};

export default PrivacyOvereenkomst;
