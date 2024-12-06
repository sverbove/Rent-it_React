import React, { useState } from 'react';
import Navbar from '/src/Components/Navbar'; 
import Footer from '/src/Components/Footer'; 
import '/src/css/Home.css';

const Contact = () => {
    const [formData, setFormData] = useState({
        name: "",
        email: "",
        message: "",
    });

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch("/api/ContactController", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(formData),
            });

            if (response.ok) {
                alert("Bericht succesvol verzonden!");
                setFormData({ name: "", email: "", message: "" }); // Reset formulier
            } else {
                alert("Er is een fout opgetreden. Probeer het opnieuw.");
            }
        } catch (error) {
            console.error("Error:", error);
            alert("Er is een probleem met het verzenden van het formulier.");
        }
    };

    return (
        <>
            {/* Navbar toevoegen */}
            <Navbar />

            {/* Contactpagina-inhoud */}
            <div style={{ padding: "2rem", maxWidth: "600px", margin: "auto" }}>
                <h1>Contact</h1>
                <p>
                    Neem contact met ons op via het formulier hieronder of gebruik onze
                    contactgegevens aan de rechterkant.
                </p>

                <div style={{ display: "flex", justifyContent: "space-between" }}>
                    {/* Contactgegevens */}
                    <div style={{ flex: "1", marginRight: "1rem" }}>
                        <h2>Onze Gegevens</h2>
                        <p>
                            <strong>Adres:</strong>
                            <br />
                            Johanna Westerdijkplein 75, 2521 EP
                            Den Haag, Nederland
                        </p>
                        <p>
                            <strong>Email:</strong>
                            <br />
                            <a href="mailto:informatie.rentit@gmail.com">informatie.rentit@gmail.com</a>
                        </p>
                    </div>

                    {/* Contactformulier */}
                    <div style={{ flex: "1", marginLeft: "1rem" }}>
                        <h2>Stuur ons een bericht</h2>
                        <form onSubmit={handleSubmit}>
                            <div style={{ marginBottom: "1rem" }}>
                                <label>Naam:</label>
                                <br />
                                <input
                                    type="text"
                                    name="name"
                                    value={formData.name}
                                    onChange={handleChange}
                                    style={{ width: "100%", padding: "0.5rem" }}
                                    required
                                />
                            </div>
                            <div style={{ marginBottom: "1rem" }}>
                                <label>Email:</label>
                                <br />
                                <input
                                    type="email"
                                    name="email"
                                    value={formData.email}
                                    onChange={handleChange}
                                    style={{ width: "100%", padding: "0.5rem" }}
                                    required
                                />
                            </div>
                            <div style={{ marginBottom: "1rem" }}>
                                <label>Bericht:</label>
                                <br />
                                <textarea
                                    name="message"
                                    value={formData.message}
                                    onChange={handleChange}
                                    style={{ width: "100%", padding: "0.5rem" }}
                                    rows="5"
                                    required
                                ></textarea>
                            </div>
                            <button
                                type="submit"
                                style={{
                                    backgroundColor: "#007BFF",
                                    color: "#FFF",
                                    padding: "0.5rem 1rem",
                                    border: "none",
                                    cursor: "pointer",
                                }}
                            >
                                Verzend
                            </button>
                        </form>
                    </div>
                </div>
            </div>

            {/* Footer toevoegen */}
            <Footer />
        </>
    );
};

export default Contact;
