import React from 'react';
import Navbar from '/src/Components/Navbar'; // Zorg ervoor dat het pad klopt naar je Navbar-component

const Register = () => {
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
                    {/* Foto achtergrond */}
                    <div
                        style={{
                            backgroundImage: 'url("/src/assets/SkylineDenHaag.jpeg")',
                            backgroundSize: 'cover',
                            backgroundPosition: 'bottom',
                            position: 'absolute',
                            top: 0,
                            left: 0,
                            right: 0,
                            bottom: 0,
                            zIndex: 1,
                            opacity: '50%', // Alternatief voor opacity zonder invloed op tekst
                        }}
                    />
                </div>
            </main>
        </div>
    );
};

export default Register;
