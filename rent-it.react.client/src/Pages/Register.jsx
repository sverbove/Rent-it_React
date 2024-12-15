import React from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import RegisterForm from '/src/Components/RegisterForm';
import GoogleSignInButton from '/src/Components/GoogleSignInButton';
import '/src/css/Home.css';
import '/src/css/LogIn.css';

const Register = () => {

    const handleGoogleSignIn = () => {
        window.location.href = "/api/Login/GoogleLogin";
    }

    return (
        < div className = "mainWrapper" >
            <Navbar />

            <main className="mainSection">
                <div className="loginCard card shadow border">
                    <h1>
                        Registreer
                    </h1>

                    <RegisterForm />

                    <hr />

                    <GoogleSignInButton onClick={handleGoogleSignIn} />

                </div>
            </main>

            <Footer />
        </div >
    );
};

export default Register;
