import React from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import '/src/css/Zakelijk.css';

const Zakelijk = () => {
    return (
        <div>
            <Navbar />
            <div className="zakelijkHero">
                <h1>Zakelijk huren met gemak</h1>
                <p>Flexibele oplossingen voor bedrijven, van klein tot groot wagenparkbeheer.</p>
                <div className="zakelijkButtons">
                    <a href="#aanmelden" className="btnPrimary">Bedrijf aanmelden</a>
                    <a href="#meerInformatie" className="btnSecondary">Meer informatie</a>
                </div>
            </div>
            <section className="benefitsSection">
                <h2>Waarom Rent-IT?</h2>
                <div className="benefitsGrid">
                    <div className="benefitCard">
                        <h3>Altijd Beschikbaar</h3>
                        <p>Toegang tot een breed scala aan voertuigen, altijd en overal.</p>
                    </div>
                    <div className="benefitCard">
                        <h3>Flexibele Opties</h3>
                        <p>Geen lange verplichtingen, huur precies wat je nodig hebt.</p>
                    </div>
                    <div className="benefitCard">
                        <h3>Groene Mobiliteit</h3>
                        <p>Ondersteun duurzame initiatieven met elektrische voertuigen.</p>
                    </div>
                </div>
            </section>
            <Footer />
        </div>
    );
};

export default Zakelijk;
