import { Link } from 'react-router-dom';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import LoginForm from '/src/Components/LoginForm';
import GoogleSignInButton from '/src/Components/GoogleSignInButton';
import '/src/css/Home.css';
import '/src/css/LogIn.css';

const LogIn = () => {

    const handleGoogleSignIn = () => {
        window.location.href = "/api/Login/GoogleLogin";
    }

    return (
        <div className="mainWrapper">
            <Navbar />

            <main className="mainSection">
                <div className="loginCard card shadow border">
                    <h1>
                        Log in met je account
                    </h1>

                    <LoginForm />

                    <hr/>

                    <GoogleSignInButton onClick={handleGoogleSignIn} />

                    <p className="registerText">Nog geen account? <Link to="/Register" className="registerLink">Registreren</Link></p>
                </div>
            </main>

            <Footer />
        </div>
    ); 
};  

export default LogIn;
