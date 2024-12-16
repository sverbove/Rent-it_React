import React, { useState } from "react";
import Swal from "sweetalert2";

const AddMedewerkerForm = () => {
    const [gebruikersnaam, setGebruikersnaam] = useState("");
    const [email, setEmail] = useState("");
    const [wachtwoord, setWachtwoord] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const token = localStorage.getItem("token");

            const response = await fetch("/api/Login/add-medewerker-to-subscription", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ gebruikersnaam, email, wachtwoord }),
            });

            if (response.ok) {
                Swal.fire({
                    icon: "success",
                    title: "Medewerker toegevoegd",
                    text: `Medewerker ${gebruikersnaam} is succesvol gekoppeld aan het abonnement!`,
                });
                setGebruikersnaam("");
                setEmail("");
                setWachtwoord("");
            } else {
                const error = await response.text();
                Swal.fire({
                    icon: "error",
                    title: "Fout",
                    text: error || "Kon medewerker niet toevoegen.",
                });
            }
        } catch (err) {
            Swal.fire({
                icon: "error",
                title: "Verbindingsfout",
                text: "Kan geen verbinding maken met de server.",
            });
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Gebruikersnaam: </label>
                <input
                    type="text"
                    value={gebruikersnaam}
                    onChange={(e) => setGebruikersnaam(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>Email: </label>
                <input
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>Wachtwoord: </label>
                <input
                    type="password"
                    value={wachtwoord}
                    onChange={(e) => setWachtwoord(e.target.value)}
                    required
                />
            </div>

            <button type="submit">Medewerker Toevoegen</button>
        </form>
    );
};

export default AddMedewerkerForm;