import React, { useState } from "react";
import Swal from "sweetalert2";
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import "/src/css/LogIn.css";

const RegisterForm = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [name, setName] = useState("");
    const [userType, setUserType] = useState("Particuliere Klant");

    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (password.length < 8) {
            Swal.fire({
                icon: "error",
                title: "Ongeldig wachtwoord",
                text: "Het wachtwoord moet minstens 8 karakters lang zijn",
            });
            return;
        }

        if (password !== confirmPassword) {
            Swal.fire({
                icon: "error",
                title: "Wachtwoorden komen niet overeen",
                text: "Beide wachtwoorden moeten hetzelfde zijn",
            });
            return;
        }

        if (userType === "Zakelijke Klant" && email.endsWith("@gmail.com")) {
            Swal.fire({
                icon: "error",
                title: "Ongeldig e-mailadres",
                text: "Zakelijk email-adres mag niet op '@gmail' eindigen",
            });
            return;
        }

        // Prepare account data
        const accountData = {
            Gebruikersnaam: name,
            Email: email,
            Wachtwoord: password,
            Rol: userType,
            IsActief: true,
        };

        try {
            const response = await fetch('/api/Account/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(accountData),
            });

            if (response.ok) {
                Swal.fire({
                    icon: "success",
                    title: "Registratie gelukt",
                    text: "Uw account is succesvol aangemaakt!",
                });

                setName("");
                setEmail("");
                setPassword("");
                setConfirmPassword("");
                setUserType("Particuliere Klant");

                navigate("/login"); // Redirect to login page
            } else {
                const errorMessage = await response.text();
                console.error("Debug: Error message", errorMessage);
                Swal.fire({
                    icon: "error",
                    title: "Registratie mislukt",
                    text: errorMessage || "Er is een fout opgetreden.",
                });
            }
        } catch (error) {
            console.error("Registratiefout:", error);
            Swal.fire({
                icon: "error",
                title: "Registratie mislukt",
                text: "Kan geen verbinding maken met de server. Controleer uw internetverbinding.",
            });
        }
    };

    return (
        <form method="post" onSubmit={handleSubmit} className="registerFormBox">
            <div className="form-row tableRow">
                <div className="col tableColumn">
                    <input
                        type="text"
                        className="form-control registerFormControl"
                        id="name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        placeholder="Naam"
                        required
                    />
                </div>
                <div className="col tableColumn">
                    <input
                        type="email"
                        className="form-control registerFormControl"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="E-mailadres"
                        required
                    />
                </div>
            </div>

            <div className="form-row tableRow">
                <div className="col tableColumn">
                    <input
                        type="password"
                        className="form-control registerFormControl"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Wachtwoord"
                        required
                    />
                </div>
                <div className="col tableColumn">
                    <input
                        type="password"
                        className="form-control registerFormControl"
                        id="confirm-password"
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        placeholder="Herhaal wachtwoord"
                        required
                    />
                </div>
            </div>

            <div className="form-row tableRow">
                <div className="col tableColumn">
                    <select
                        className="form-control registerFormControl"
                        id="user-type"
                        value={userType}
                        onChange={(e) => setUserType(e.target.value)}
                        required
                    >
                        <option value="Particuliere Klant">Particuliere Klant</option>
                        <option value="Zakelijke Klant">Zakelijke Klant</option>
                    </select>
                </div>
            </div>

            <br />

            <div className="row text-end tableRow">
                <div className="col-2 text-end tableColumn">
                    <button
                        type="submit"
                        className="btn btn-primary form-control registerFormControl"
                    >
                        Registreer
                    </button>
                </div>
            </div>
        </form>
    );
};

export default RegisterForm;