import React from 'react';
import { Link } from 'react-router-dom';

const HomePage = () => {
    return (
        <div style={{ textAlign: 'center', padding: '2rem' }}>
            <h1>Welcome to Rent-it</h1>
            <p>Your one-stop solution for renting anything!</p>
            <div style={{ marginTop: '2rem' }}>
                <Link to="/browse">
                    <button style={{ padding: '1rem 2rem', fontSize: '1rem', cursor: 'pointer' }}>
                        Browse Listings
                    </button>
                </Link>
                <Link to="/add-listing" style={{ marginLeft: '1rem' }}>
                    <button style={{ padding: '1rem 2rem', fontSize: '1rem', cursor: 'pointer' }}>
                        Add a Listing
                    </button>
                </Link>
            </div>
        </div>
    );
};

export default HomePage;
