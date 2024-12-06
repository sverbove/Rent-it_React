import React from 'react';
import Navbar from '/src/Components/Navbar'; // Zorg ervoor dat het pad klopt naar je Navbar-component

const Contact = () => {
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
                        position: 'absolute',
                        marginLeft: '20px',
                        marginTop: '0px',
                        textAlign: 'left',
                        color: '#000000',
                    }}
                >
                    <h1 style={{
                        fontSize: '30px', margin: 0
                    }}> test</h1>
                </div>
            </main>
        </div>
    );
};

export default Contact;
