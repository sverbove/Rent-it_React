import React from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import '/src/css/Home.css';

const Zakelijk = () => {
    return (
        <div className="mainWrapper">
            <Navbar />

            <main className="mainSection">
                <div className="backdrop">
                    <div className="backdropImage" />
                </div>
            </main>

            <Footer />
        </div>
    );
};

export default Zakelijk;
                    