import React from 'react';
import Navbar from '/src/Components/Navbar'; // Zorg ervoor dat het pad klopt naar je Navbar-component

const Home = () => {
    return (
        <div style={{
            fontFamily: 'Arial, sans-serif',
            backgroundColor: '#D9D9D9',
            height: '100vh',
            margin: '0',
            padding: '0',
            display: 'flex',
            flexDirection: 'column',
            boxSizing: 'border-box',
        }}>
            {/* Gebruik de Navbar-component */}
            <Navbar />

            <main style={{ position: 'relative', textAlign: 'center', color: '#fff', flex: 1 }}>
                <div
                    style={{
                        position: 'relative',
                        height: '70vh',
                        marginTop: '20px',
                        marginBottom: '10px',
                        overflow: 'hidden',
                    }}
                >
                    {/* Background Image */}
                    <div
                        style={{
                            backgroundImage: 'url("/src/assets/SkylineDenHaag.jpeg")',
                            backgroundSize: 'cover',
                            backgroundPosition: 'center',
                            position: 'absolute',
                            top: 0,
                            left: 0,
                            right: 0,
                            bottom: 0,
                            zIndex: 1,
                            opacity: '50%', // Alternatief voor opacity zonder invloed op tekst
                        }}
                    />

                    {/* Text Section */}
                    <div
                        style={{
                            position: 'absolute',
                            bottom: '120px',
                            left: '20px',
                            zIndex: 2,
                            textAlign: 'left',
                            padding: '10px 15px',
                            color: '#000000',
                        }}
                    >
                        <h1 style={{ fontSize: '100px', margin: 0 }}>Particulier of zakelijk,</h1>
                    </div>
                    <div
                        style={{
                            position: 'absolute',
                            bottom: '20px',
                            left: '20px',
                            zIndex: 2,
                            textAlign: 'left',
                            padding: '10px 15px',
                            color: '#D9D9D9',
                        }}
                    >
                        <h2 style={{ fontSize: '100px', margin: 0 }}>iedereen is welkom.</h2>
                    </div>
                </div>

                <img
                    src="/src/assets/ZakenmanHomePage.png"
                    alt="Person"
                    style={{
                        position: 'absolute',
                        bottom: '0px',
                        right: '25px',
                        width: '30%',
                        height: 'auto',
                        zIndex: 3, // Zorg dat de afbeelding boven de tekst ligt
                    }}
                />
            </main>

            <footer
                style={{
                    backgroundColor: '#FFFFFF',
                    padding: '10px 20px',
                    textAlign: 'center',
                }}
            >
                <p style={{ margin: 0 }}>@2024 Rent-It. Alle rechten voorbehouden.</p>
            </footer>
        </div>
    );
};

export default Home;
