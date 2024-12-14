import React, { useState } from 'react';
import Swal from 'sweetalert2';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import '/src/css/Home.css';
import '/src/css/AbonnementenInfo.css';

const AbonnementenInfo = () => {
    const [selectedPlan, setSelectedPlan] = useState('');
    const [formData, setFormData] = useState({
        bedrijfsnaam: '',
        adres: '',
        kvkNummer: ''
    });

    const [errors, setErrors] = useState('');

    const handleCheckboxChange = (plan) => {
        setSelectedPlan(plan);
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const validateForm = () => {
        if (!selectedPlan) {
            Swal.fire({
                icon: 'warning',
                title: 'Formulierfout',
                text: 'Selecteer een abonnementsvorm.'
            });
            return false;
        }
        // Kijk of het KVK-nummer niet precies 8 tekens bevat of dat er niet alleen nummer instaan
        if (formData.kvkNummer.length !== 8 || !/^\d+$/.test(formData.kvkNummer)) {
            Swal.fire({
                icon: 'error',
                title: 'Ongeldig KVK-nummer',
                text: 'Het KVK-nummer moet precies 8 cijfers hebben.'
            })
            return false;
        }
        return true;
    };

    const isFormComplete = () => {
        return (
            formData.bedrijfsnaam.trim() &&
            formData.adres.trim() &&
            formData.kvkNummer.trim() &&
            selectedPlan
        );
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!validateForm()) return;

        const abonnementData = {
            bedrijfsnaam: formData.bedrijfsnaam,
            adres: formData.adres,
            kvkNummer: formData.kvkNummer,
            abonnementsvorm: selectedPlan
        };

        try {
            const response = await fetch('/api/Abonnement', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(abonnementData)
            });

            const data = await response.json();
            if (response.ok) {
                Swal.fire({
                    icon: 'success',
                    title: 'Abonnement aangemaakt',
                    text: `Het abonnement is succesvol aangemaakt!`
                });
                setFormData({ bedrijfsnaam: '', adres: '', kvkNummer: '' });
                setSelectedPlan('');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Fout bij aanmaken',
                    text: 'Er is een probleem opgetreden bij het aanmaken van het abonnement.'
                });
            }
        } catch (error) {
            Swal.fire({
                icon: 'error',
                title: 'Serverfout',
                text: 'Kan geen verbinding maken met de server. Probeer het later opnieuw.'
            });
        }
    };

    return (
        <div className="mainWrapper">
            <Navbar />
            <main className="mainSection">
                <div className="container">
                    <h1 className="title">Kies je abonnement</h1>

                    <div className="cardsContainer">
                        <div className={`subscriptionCard ${selectedPlan === 'prepaid' ? 'selected' : ''}`}>
                            <h2>Pre-Paid</h2>
                            <p>
                                Betaal vooruit voor een vast aantal huurdagen. Ideaal als je precies weet hoe lang je voertuigen nodig hebt.
                            </p>
                            <label className="checkboxContainer">
                                <input
                                    type="checkbox"
                                    checked={selectedPlan === 'prepaid'}
                                    onChange={() => handleCheckboxChange('prepaid')}
                                />
                                <span>Selecteer Pre-Paid</span>
                            </label>
                        </div>

                        <div className={`subscriptionCard ${selectedPlan === 'payg' ? 'selected' : ''}`}>
                            <h2>Pay-As-You-Go</h2>
                            <p>
                                Betaal een vast maandelijks bedrag en krijg korting op elke huur. Flexibel en handig voor onverwacht gebruik.
                            </p>
                            <label className="checkboxContainer">
                                <input
                                    type="checkbox"
                                    checked={selectedPlan === 'payg'}
                                    onChange={() => handleCheckboxChange('payg')}
                                />
                                <span>Selecteer Pay-As-You-Go</span>
                            </label>
                        </div>
                    </div>

                    <div className="formSection">
                        <h2>Vul je bedrijfsgegevens in</h2>
                        {errors && <p className="errorMessage">{errors}</p>}
                        <form onSubmit={handleSubmit} className="formInfo">
                            <label>Bedrijfsnaam:</label>
                            <input
                                type="text"
                                name="bedrijfsnaam"
                                value={formData.bedrijfsnaam}
                                onChange={handleInputChange}
                                required
                            />

                            <label>Adres:</label>
                            <input
                                type="text"
                                name="adres"
                                value={formData.adres}
                                onChange={handleInputChange}
                                required
                            />

                            <label>KVK-Nummer:</label>
                            <input
                                type="text"
                                name="kvkNummer"
                                value={formData.kvkNummer}
                                onChange={handleInputChange}
                                required
                            />

                            <button
                                type="submit"
                                className={`submitButton ${isFormComplete() ? 'enabled' : ''}`}
                                disabled={!isFormComplete()}
                            >
                                Abonnement Aanmaken
                            </button>
                        </form>
                    </div>
                </div>
            </main>
            <Footer />
        </div>
    );
};

export default AbonnementenInfo;