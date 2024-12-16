import React, { useEffect, useState } from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import AddMedewerkerForm from '/src/Components/AddMedewerkerForm';
import '/src/css/Zakelijk.css';
import '/src/css/Home.css';

const Zakelijk = () => {
    const [abonnementsvorm, setAbonnementsvorm] = useState("");
    const [canAddMedewerkers, setCanAddMedewerkers] = useState(false);

    useEffect(() => {
        const fetchSubscriptionStatus = async () => {
            try {
                const token = localStorage.getItem("token");

                const response = await fetch("/api/Abonnement/GetSubscriptionStatus", {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });

                if (response.ok) {
                    const data = await response.json();
                    setAbonnementsvorm(data.abonnementsvorm);
                    setCanAddMedewerkers(data.canAddMedewerkers);
                } else {
                    console.error("Failed to fetch subscription status");
                }
            } catch (error) {
                console.error("Error fetching subscription status:", error);
            }
        };

        fetchSubscriptionStatus();
    }, []);

    return (
        <div className="mainWrapper">
            <Navbar />  
            <div className="particulierHero">
                <h1>Zakelijk huren met gemak</h1>
                <p>Flexibele oplossingen voor bedrijven, van klein tot groot wagenparkbeheer.</p>
                <div className="zakelijkButtons">
                    <a href="/Register" className="btnPrimary">Bedrijf registreren</a>
                    <a href="/Info" className="btnSecondary">Meer informatie</a>
                </div>
            </div>
            <section className="subscriptionSection">
                <h2>Abonnementen beheren:</h2>
                <br/>
                <a href="/AbonnementenInfo" className="btnPrimary">Abonnement maken</a>

                <h2>Abonnement status:</h2>
                <p>{abonnementsvorm}</p>
                {!canAddMedewerkers && (
                    <p style={{ color: "red" }}>
                        Je kunt nog geen medewerkers toevoegen. Kies een geschikt abonnement.
                    </p>
                )}
            </section>

            {canAddMedewerkers && (
                <section className="addMedewerkerSection">
                    <h2>Voeg medewerkers toe</h2>
                    <br />
                    <AddMedewerkerForm />
                </section>
            )}

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
