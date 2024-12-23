import React from 'react';
import { Link } from 'react-router-dom';

const Navbar = () => {
    return (
        <header style={{ position: top, display: 'flex', alignItems: 'center', justifyContent: 'space-between', padding: '10px 20px', backgroundColor: '#D9D9D9' }}>
            {/* Logo */}
            <div style={{ display: 'flex', alignItems: 'center' }}>
                <Link to="/Home" style={{ textDecoration: 'none' }}>
                    <img
                        src="/src/assets/Logo.png"
                        alt="Logo"
                        style={{ width: 'auto', height: '40px', marginRight: '10px' }}
                    />
                </Link>
                <span style={{ fontSize: '20px', fontWeight: 'bold' }}>I</span>
            </div>

            {/* Links */}
            <nav style={{ display: 'flex', gap: '15px' }}>
                <Link to="/Home" style={{
                    textDecoration: 'none',
                    fontSize: '24px',
                    fontWeight: 'bold',
                    marginLeft: '20px',
                    marginTop: '0px',
                    color: '#000',
                    borderRadius: '5px',
                    cursor: 'pointer',
                    transition: 'background-color 0.3s',
                }}>Home</Link>
                <Link to="/Zakelijk" style={{
                    textDecoration: 'none',
                    fontSize: '24px',
                    fontWeight: 'bold',
                    marginLeft: '50px',
                    marginTop: '0px',
                    color: '#000',
                    borderRadius: '5px',
                    cursor: 'pointer',
                    transition: 'background-color 0.3s',
                }}>Zakelijk</Link>
                <Link to="/Particulier" style={{
                    textDecoration: 'none',
                    fontSize: '24px',
                    fontWeight: 'bold',
                    marginLeft: '50px',
                    marginTop: '0px',
                    color: '#000',
                    borderRadius: '5px',
                    cursor: 'pointer',
                    transition: 'background-color 0.3s',
                }}>Particulier</Link>
                <Link to="/Info" style={{
                    textDecoration: 'none',
                    fontSize: '24px',
                    fontWeight: 'bold',
                    marginLeft: '50px',
                    marginTop: '0px',
                    color: '#000',
                    borderRadius: '5px',
                    cursor: 'pointer',
                    transition: 'background-color 0.3s',
                }}>Info</Link>
                <Link to="/Contact" style={{
                    textDecoration: 'none',
                    fontSize: '24px',
                    fontWeight: 'bold',
                    marginLeft: '50px',
                    marginTop: '0px',
                    color: '#000',
                    borderRadius: '5px',
                    cursor: 'pointer',
                    transition: 'background-color 0.3s',
                }}>Contact</Link>
                <Link to="/Temp" style={{
                    textDecoration: 'none',
                    fontSize: '24px',
                    fontWeight: 'bold',
                    marginLeft: '20px',
                    marginTop: '0px',
                    color: '#000',
                    borderRadius: '5px',
                    cursor: 'pointer',
                    transition: 'background-color 0.3s',
                }}>Temp</Link>
            </nav>

            <Link to="/Login" style={{
                textDecoration: 'none',
                fontSize: '24px',
                fontWeight: 'bold',
                marginRight: '10px',
                marginTop: '0px',
                color: '#000',
                borderRadius: '5px',
                cursor: 'pointer',
                transition: 'background-color 0.3s',
                marginLeft: 'auto',
            }}>Log In</Link>
        </header>
    );
};

export default Navbar;
