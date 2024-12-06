import React from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import '/src/css/Home.css';

const Home = () => {
    return (
        <div className="mainWrapper">
            <Navbar />

            <main className="mainSection">
                <div className = "backdrop">
                    <div className="backdropImage"/>

                    <div className="textSection">
                        <h1 className="firstHeaderLine">Particulier of zakelijk,</h1>
                        <h1 className="secondHeaderLine">iedereen is welkom.</h1>
                    </div>
                </div>

                <img src="/src/assets/ZakenmanHomePage.png" alt="Image of a businessman" className="zakenmanImage"/>
            </main>

            <Footer />
        </div>
    );
};

export default Home;
