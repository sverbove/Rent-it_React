import React, { useState } from 'react';
import '/src/css/LogIn.css';

const RegisterForm = () => {
    /* useState to update the input fields */
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Submitted email:', email);
        console.log('Submitted password:', password);
        console.log('Confirmed password:', confirmPassword);
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

            <div className="col-10">
                <input
                    type="password"
                    className="form-control"
                    id="confirm-password"
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                    placeholder="Herhaal wachtwoord"
                    required
                />
            </div>

            <br />

            <div className="row text-end">
                <div className="col-2 text-end">
                    <button type="submit" className="btn btn-primary form-control">
                        Registreer
                    </button>
                </div>
            </div>
        </form>
    );
};

export default RegisterForm;
