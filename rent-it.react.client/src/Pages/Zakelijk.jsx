import React from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import '/src/css/Zakelijk.css';
import '/src/css/Home.css';

const Zakelijk = () => {
    //hello
    return (
        <div className="mainWrapper">
            <Navbar />  
            <div className="particulierHero">
                <h1>Zakelijk huren met gemak</h1>
                <p>Flexibele oplossingen voor bedrijven, van klein tot groot wagenparkbeheer.</p>
                <div className="zakelijkButtons">
                    <a href="/Register" className="btnPrimary">Bedrijf aanmelden</a>
                    <a href="/Info" className="btnSecondary">Meer informatie</a>
                </div>
            </div>
            <section className="benefitsSection">
                <h2>Waarom Rent-It?</h2>
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
                        <h3>Morgen al rijden</h3>
                        <p>Snel toegang tot het perfecte voertuig voor jou!</p>
                    </div>
                </div>
            </section>
            <Footer />
        </div>
    );
};

export default Zakelijk;
