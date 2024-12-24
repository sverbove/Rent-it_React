import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';
import '/src/css/LogIn.css';
import { jwtDecode } from 'jwt-decode';

const LoginForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('/api/Login/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, password }),
            });

            if (response.ok) {
                const data = await response.json();

                localStorage.setItem("token", data.token);
                const decoded = jwtDecode(data.token);

                Swal.fire({
                    icon: 'success',
                    title: 'Login succesvol',
                    text: `Welkom terug!`,
                });

                navigate('/home'); // Redirect to home page
            } else {
                const errorMessage = await response.text();

                Swal.fire({
                    icon: 'error',
                    title: 'Login mislukt',
                    text: errorMessage || 'Onjuiste e-mail of wachtwoord.',
                });
            }
        } catch (error) {

            Swal.fire({
                icon: 'error',
                title: 'Fout bij verbinding',
                text: 'Kan geen verbinding maken met de server.',
            });
        }
    };

    return (
        <form method="post" onSubmit={handleSubmit} className="loginFormBox">
            <div className="col-10">
                <input
                    type="text"
                    className="form-control"
                    id="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder="E-mailadres"
                    required
                />
            </div>

            <div className="col-10">
                <input
                    type="password"
                    className="form-control"
                    id="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    placeholder="Wachtwoord"
                    required
                />
            </div>

            <br />

            <div className="row text-end">
                <div className="col-2 text-end">
                    <button type="submit" className="btn btn-primary form-control">
                        Login
                    </button>
                </div>
            </div>
        </form>
    );
};

export default LoginForm;