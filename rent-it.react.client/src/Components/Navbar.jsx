import React, {useState, useEffect} from 'react';
import { Link, useNavigate } from 'react-router-dom';

const Navbar = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (token) {
            fetch("/api/Login/ValidateToken", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
            })
                .then((response) => {
                    if (response.ok) {
                        setIsLoggedIn(true);
                    } else {
                        localStorage.removeItem("token");
                        setIsLoggedIn(false);
                    }
                })
                .catch((err) => {
                    console.error("Error validating token:", err);
                    localStorage.removeItem("token");
                    setIsLoggedIn(false);
                });
        } else {
            setIsLoggedIn(false);
        }
    }, []);

    const handleLogout = () => {
        localStorage.removeItem("token");
        setIsLoggedIn(false);
        navigate("/Home");
    };

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

            {/* Render login OR logout button + profile */}
            {isLoggedIn ? (
                <div style={{ display: "flex", alignItems: "center", marginLeft: "auto" }}>
                    <button
                        onClick={() => navigate("/UserInfo")}
                        style={{
                            textDecoration: "none",
                            fontSize: "18px",
                            fontWeight: "bold",
                            padding: "5px 20px",
                            color: "#000",
                            backgroundColor: "transparent",
                            border: "none",
                            cursor: "pointer",
                        }}
                    >
                        Profiel
                    </button>
                    <button
                        onClick={handleLogout}
                        style={{
                            textDecoration: "none",
                            fontSize: "18px",
                            fontWeight: "bold",
                            marginRight: "10px",
                            marginTop: "0px",
                            padding: "10px 20px",
                            color: "#fff",
                            borderRadius: "5px",
                            cursor: "pointer",
                            marginLeft: "auto",
                            backgroundColor: "#D9534F",
                            border: "none",
                            boxShadow: "0 4px 8px rgba(0, 0, 0, 0.2)",
                            transition: "transform 0.2s, background-color 0.3s",
                        }}
                        onMouseOver={(e) => (e.target.style.backgroundColor = "#C9302C")}
                        onMouseOut={(e) => (e.target.style.backgroundColor = "#D9534F")}
                        onFocus={(e) => (e.target.style.backgroundColor = "#C9302C")}
                    >
                        Log Out
                    </button>
                </div>
            ) : (
                <Link
                    to="/Login"
                    style={{
                        textDecoration: "none",
                        fontSize: "18px",
                        fontWeight: "bold",
                        marginRight: "10px",
                        marginTop: "0px",
                        padding: "10px 20px",
                        color: "#454545",
                        borderRadius: "5px",
                        cursor: "pointer",
                        marginLeft: "auto",
                        backgroundColor: "#B9B9B9",
                        border: "none",
                        boxShadow: "0 4px 8px rgba(0, 0, 0, 0.2)",
                        transition: "transform 0.2s, background-color 0.3s",
                    }}
                    onMouseOver={(e) => (e.target.style.backgroundColor = "#A8A8A8")}
                    onMouseOut={(e) => (e.target.style.backgroundColor = "#B9B9B9")}
                >
                    Log In
                </Link>
            )}
        </header>
    );
};

export default Navbar;
