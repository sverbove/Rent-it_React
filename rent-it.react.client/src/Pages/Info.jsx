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
                <div>
                    <p>Vragen?</p>
                    <a href="/contact">Neem contact op</a>
                </div>
            </div>
            <Footer />
        </div>  
    );
};

export default Info;
