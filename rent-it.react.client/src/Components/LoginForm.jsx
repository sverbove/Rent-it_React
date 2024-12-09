import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';
import '/src/css/LogIn.css';

const LoginForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log('Debug: Form submitted');
        console.log('Debug: Email:', email);
        console.log('Debug: Password:', password);

        try {
            console.log('Debug: Making API request to /api/Login/login');
            const response = await fetch('/api/Login/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, password }),
            });

            console.log('Debug: Response received:', response);

            if (response.ok) {
                console.log('Debug: Response status is OK');
                const data = await response.json();
                console.log('Debug: Response data:', data);

                Swal.fire({
                    icon: 'success',
                    title: 'Login succesvol',
                    text: `Welkom terug, ${data.gebruikersnaam}!`,
                });

                localStorage.setItem('token', data.token);
                console.log('Debug: Token saved to localStorage');

                // Redirect to /home
                navigate('/home');
                console.log('Debug: Navigated to /home');
            } else {
                const errorMessage = await response.text();
                console.error('Debug: Response not OK. Error message:', errorMessage);

                Swal.fire({
                    icon: 'error',
                    title: 'Login mislukt',
                    text: errorMessage || 'Onjuiste e-mail of wachtwoord.',
                });
            }
        } catch (error) {
            console.error('Debug: API request failed:', error);

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