import React from 'react';
import { Link } from 'react-router-dom';

const Footer = () => {
    return (
        <footer style = {{ backgroundColor: 'white', padding: '10px 20px', textAlign: 'center' }}>
            <p style={{ margin: 0 }}>&copy; 2024 Rent-It. Alle rechten voorbehouden.</p>
        </footer>
    );
};

export default Footer;
