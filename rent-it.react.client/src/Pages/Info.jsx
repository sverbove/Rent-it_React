import React from 'react';
import '/src/css/Info.css';
import Home from '/src/Pages/Home.jsx';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';

const Info = () => {
    return (
        <div className="info-container">
            <Navbar />
            <h1 className="info-title">Informatie</h1>
            <div className="info-cards">
                <div className="info-card">
                    <h2>Ons Bedrijf</h2>
                    <p>
                        Bij Rent-It maken we het huren van een auto, caravan of camper toegankelijk voor iedereen. We zijn opgericht op 5 december
                        2024 door Nicolaas van Myra. Heer Nicolaas was hierheen gekomen op de boot en zocht een auto voor een roadtrip terug naar Spanje.
                        Het was op dit moment dat Nico erachter kwam dat hij de ervaring van autos huren verschrikkelijk omslachtig vindt. Veel overbodig
                        contact, ingewikkeld huurproces en tevens hoge prijzen gaven hem de indruk dat hij beter toch maar met de stoomboot terug kon.
                        Het was na deze ervaring dat Nico besloot zelf een voertuigverhuurbedrijf op te zetten. Hij begon met een aantal kleinere hatchbacks,
                        maar al snel bloeide het uit tot de magnifieke vloot aan autos, campers en caravans die je vandaag de dag op de site ziet.
                    </p>
                </div>
                <div className="info-card">
                    <h2>Onze Werkwijze</h2>
                    <p>
                        Een voertuig huren bij Rent-It is enorm eenvoudig. Op onze site kan je de opties bekijken voor particuliere en zakelijke klanten. Als
                        je geinteresseerd bent in onze service, kan je een account aanmaken. Hier vragen we je om een aantal stukken informatie. Nog niks belangrijks hoor,
                        dat komt later pas. Als je bent ingelogd begint het echte werk. Nu kan je een kijkje nemen in onze vloot aan voertuigen. Je kan hierbij sorteren op
                        verschillende factoren, zodat je precies vindt wat je zoekt! Heb je een keuze kunnen maken? Kies je datum en klik op Huren. Hier krijg je een notificatie
                        van, en dan kan je hem op de datum van afgifte komen ophalen! De prijzen hangen van een aantal factoren af. Soort voertuig, huurtermijn en beschikbaarheid
                        van voertuigen zijn hier enkele van.
                    </p>
                </div>
                <div className="info-card">
                    <h2>Abonnementen</h2>
                    <p>
                        Bij Rent-It bieden we voor zakelijke klanten meerdere soorten abonnementen aan. Met Pay-As-You-Go betaal je alleen voor wat je gebruikt. Dit is ideaal als je niet precies
                        weet wat je gaat huren. Dit is een maandelijks bedrag, afhankelijk van de voertuigen die je in gebruik hebt bij je bedrijf
                    </p>
                    <p>
                        Met een pre-paid abonnement betaal je vooraf een vast bedrag. Dit is ideaal als je precies weet wat je gaat huren en hoe lang je er gebruik van gaat maken.
                        Dit is een stuk minder flexibel, maar past goed bij grotere bedrijven waar berekend kan worden wat er nodig is.
                    </p>
                </div>
            </div>

            <div className="extra-info">
                <div className="extra-card">
                    <h2>Prijzen</h2>
                    <p>
                        De kosten voor het huren van onze voertuigen zijn afhankelijk van het type voertuig, duur van de huurperiode en de verwachte afstand die je gaat afleggen met het voertuig.
                        Autos beginnen bij 20EU per dag, caravans bij 40EU per dag en campers bij 60EU per dag.
                    </p>
                </div>
                <div className="extra-card">
                    <h2>Voorwaarden</h2>
                    <p>
                        Om bij ons een auto- of camper te huren moet je minimaal twee jaar in bezit zijn van je B-rijbewijs. Voor caravans geldt dit voor je BE-rijbewijs. Ook moet je minimaal 21 jaar
                        oud zijn en moet je in staat zijn een rijbewijs EN identiteitsbewijs of een paspoort te laten zien.

                    </p>
                </div>
            </div>

            <div>
                <p>Vragen?</p>
                <a href="/contact">Neem contact op</a>
            </div>

            <Footer />

        </div>
    );
};

export default Info;
