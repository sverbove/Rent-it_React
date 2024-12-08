import React from 'react';
import { Link } from 'react-router-dom';

const Footer = () => {
    return (
        <footer
            style={{
                backgroundColor: 'white',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center', 
                padding: '10px 20px',
                bottom: '0',
            }}
        >
            <p
                style={{
                    position: 'absolute',
                    margin: 0,
                    color: 'black',
                    fontWeight: 'bold',
                }}
            >
                &copy; 2024 Rent-It. Alle rechten voorbehouden.
            </p>

            <Link
                to="/AlgemeneVoorwaarden"
                style={{
                    position: 'relative',
                    marginLeft: '600px', 
                    textDecoration: 'none',
                    fontWeight: 'bold',
                    color: '#000',
                    cursor: 'pointer',
                    transition: 'color 0.3s',
                }}
                onMouseEnter={(e) => (e.target.style.color = '#555')}
                onMouseLeave={(e) => (e.target.style.color = '#000')}
            >
                Algemene Voorwaarden
            </Link>

            <Link
                to="/PrivacyOvereenkomst"
                style={{
                    position: 'absolute',
                    marginLeft: '-600px',
                    textDecoration: 'none',
                    fontWeight: 'bold',
                    color: '#000',
                    cursor: 'pointer',
                    transition: 'color 0.3s',
                }}
                onMouseEnter={(e) => (e.target.style.color = '#555')}
                onMouseLeave={(e) => (e.target.style.color = '#000')}
            >
                Privacy Overeenkomst
            </Link>
        </footer>
    );
};

export default Footer;
