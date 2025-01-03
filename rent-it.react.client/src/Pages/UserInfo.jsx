import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import React, { useState, useEffect } from "react";
import Swal from 'sweetalert2';
import '/src/css/UserInfo.css';

const UserInfo = () => {
    const [userDetails, setUserDetails] = useState({});
    const [loading, setLoading] = useState(true);
    const [showPasswordForm, setShowPasswordForm] = useState(false);
    const [oldPassword, setOldPassword] = useState("");
    const [newPassword, setNewPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [statusMessage, setStatusMessage] = useState("");
    const [editModeEmail, setEditModeEmail] = useState(false);
    const [editModeAddress, setEditModeAddress] = useState(false);
    const [updatedEmail, setUpdatedEmail] = useState("");
    const [updatedAddress, setUpdatedAddress] = useState("");

    useEffect(() => {
        const token = localStorage.getItem("token");

        fetch("/api/Login/GetUserDetails", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
        })
            .then((response) => response.json())
            .then((data) => {
                setUserDetails(data);
                setUpdatedEmail(data.email);
                setUpdatedAddress(data.subscription?.adres || "");
                setLoading(false);
            })
            .catch((err) => {
                console.error("Error fetching user details:", err);
                setLoading(false);
            });
    }, []);

    const handleSaveEmail = async () => {
        const token = localStorage.getItem("token");

        try {
            const response = await fetch("/api/Login/UpdateEmail", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ email: updatedEmail }),
            });

            if (response.ok) {
                const updatedData = await response.json();
                setUserDetails(updatedData);
                setEditModeEmail(false);

                Swal.fire({
                    icon: 'success',
                    title: 'Email bijgewerkt',
                    text: 'Je e-mailadres is succesvol bijgewerkt.',
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Bijwerken niet gelukt',
                    text: 'Probeer het opnieuw.',
                });
            }
        } catch (error) {
            Swal.fire({
                icon: 'error',
                title: 'Serverfout',
                text: 'Kan geen verbinding maken met de server.',
            });
        }
    };

    const handleSaveAddress = async () => {
        const token = localStorage.getItem("token");

        try {
            const response = await fetch("/api/Login/UpdateAddress", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ address: updatedAddress }),
            });

            if (response.ok) {
                const updatedData = await response.json();
                setUserDetails(updatedData);
                setEditModeAddress(false);

                Swal.fire({
                    icon: 'success',
                    title: 'Adres bijgewerkt',
                    text: 'Het bedrijfsadres is succesvol bijgewerkt.',
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Bijwerken niet gelukt',
                    text: 'Probeer het opnieuw.',
                });
            }
        } catch (error) {
            Swal.fire({
                icon: 'error',
                title: 'Serverfout',
                text: 'Kan geen verbinding maken met de server.',
            });
        }
    };

    const handleChangePassword = async () => {
        if (newPassword !== confirmPassword) {
            Swal.fire({
                icon: 'error',
                title: 'Wachtwoorden komen niet overeen',
                text: 'Controleer of beide wachtwoorden hetzelfde zijn.',
            });
            return;
        }

        const token = localStorage.getItem("token");

        try {
            const response = await fetch("/api/Login/ChangePassword", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ oldPassword, newPassword }),
            });

            if (response.ok) {
                setShowPasswordForm(false);
                Swal.fire({
                    icon: 'success',
                    title: 'Wachtwoord gewijzigd',
                    text: 'Uw wachtwoord is succesvol gewijzigd!',
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Fout',
                    text: 'Er is een fout opgetreden bij het wijzigen van uw wachtwoord.',
                });
            }
        } catch (error) {
            Swal.fire({
                icon: 'error',
                title: 'Serverfout',
                text: 'Kan geen verbinding maken met de server.',
            });
        }
    };

    if (loading) {
        return <p>Loading user information...</p>;
    }

    return (
        <div className="mainWrapper">
            <Navbar />

            <div className="detailsBox">
                <h1>Account details</h1>
                <p><strong>Gebruikersnaam:</strong> {userDetails.username || "Niet beschikbaar"}</p>

                {editModeEmail ? (
                    <>
                        <label>
                            Email:
                            <input
                                type="email"
                                value={updatedEmail}
                                onChange={(e) => setUpdatedEmail(e.target.value)}
                                className="editInput"
                            />
                        </label>
                        <br />
                        <button onClick={handleSaveEmail} className="saveChangesBtn">Opslaan</button>
                        <button onClick={() => setEditModeEmail(false)} className="cancelEditBtn">Annuleer</button>
                    </>
                ) : (
                    <>
                        <p><strong>Email:</strong> {userDetails.email || "Niet beschikbaar"}</p>
                        <button onClick={() => setEditModeEmail(true)} className="editDetailsBtn">Bewerk e-mail</button>
                    </>
                )}

                <p><strong>Rol:</strong> {userDetails.role || "Niet beschikbaar"}</p>
                <p><strong>Status:</strong> {userDetails.isActive ? "Actief" : "Inactief"}</p>

                <button onClick={() => setShowPasswordForm(true)} className="changePasswordBtn">
                    Wachtwoord Wijzigen
                </button>

                {/* Change password */}
                {showPasswordForm && (
                    <div className="changePasswordBox">
                        <h3>Wijzig Wachtwoord</h3>
                        <label>
                            Oud wachtwoord:
                            <input
                                type="password"
                                value={oldPassword}
                                onChange={(e) => setOldPassword(e.target.value)}
                                className="inputOldPassword"
                            />
                        </label>
                        <br />
                        <label>
                            Nieuw wachtwoord:
                            <input
                                type="password"
                                value={newPassword}
                                onChange={(e) => setNewPassword(e.target.value)}
                                className="inputNewPassword"
                            />
                        </label>
                        <br />
                        <label>
                            Bevestig nieuw wachtwoord:
                            <input
                                type="password"
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                className="inputConfirmPassword"
                            />
                        </label>
                        <br />

                        <button onClick={handleChangePassword} className="activatePasswordBtn">
                            Activeren
                        </button>

                        <button onClick={() => setShowPasswordForm(false)} className="cancelPasswordBtn">
                            Annuleer
                        </button>
                        {statusMessage && <p className="statusMessage">{statusMessage}</p>}
                    </div>
                )}

                <br />
                <br />

                {/* If subscription, print its info */}
                {userDetails.subscription && (
                    <div className="subscriptionInfoBox">
                        <h2>Abonnements- en bedrijfsinformatie</h2>
                        <p><strong>Type:</strong> {userDetails.subscription.type || "Niet beschikbaar"}</p>
                        <p>
                            <strong>Startdatum:</strong>{" "}
                            {userDetails.subscription.startDate
                                ? new Date(userDetails.subscription.startDate).toLocaleDateString()
                                : "Niet beschikbaar"}
                        </p>
                        <p><strong>Bedrijfsnaam:</strong> {userDetails.subscription.bedrijfsnaam || "Niet beschikbaar"}</p>

                        {editModeAddress ? (
                            <>
                                <label>
                                    Adres:
                                    <input
                                        type="text"
                                        value={updatedAddress}
                                        onChange={(e) => setUpdatedAddress(e.target.value)}
                                        className="editInput"
                                    />
                                </label>
                                <br />
                                <button onClick={handleSaveAddress} className="saveChangesBtn">Opslaan</button>
                                <button onClick={() => setEditModeAddress(false)} className="cancelEditBtn">Annuleer</button>
                            </>
                        ) : (
                            <>
                                <p><strong>Adres:</strong> {userDetails.subscription.adres || "Niet beschikbaar"}</p>
                                <button onClick={() => setEditModeAddress(true)} className="editDetailsBtn">Bewerk adres</button>
                            </>
                        )}

                        <p><strong>KVK Nummer:</strong> {userDetails.subscription.kvkNummer || "Niet beschikbaar"}</p>
                    </div>
                )}
            </div>
            
            <Footer />
        </div>
    );
};

export default UserInfo;
