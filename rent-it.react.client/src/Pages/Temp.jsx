import React, { useEffect, useState } from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';

const Temp = () => {
    const [vehicles, setVehicles] = useState([]);
    const [subscriptionType, setSubscriptionType] = useState('Pay-as-you-go'); // Huidig abonnementstype

    useEffect(() => {
        const dummyData = [
            { voertuigId: 1, soort: 'Auto', merk: 'Volkswagen', type: 'Golf', kleur: 'Blauw', kenteken: 'AB-123-CD', aanschafjaar: 2020, status: 'In gebruik', medewerker: 'Jan Jansen' },
            { voertuigId: 2, soort: 'Busje', merk: 'Mercedes', type: 'Sprinter', kleur: 'Wit', kenteken: 'EF-456-GH', aanschafjaar: 2019, status: 'In reparatie', medewerker: 'Piet Pietersen' },
            { voertuigId: 3, soort: 'Auto', merk: 'BMW', type: 'X5', kleur: 'Zwart', kenteken: 'IJ-789-KL', aanschafjaar: 2021, status: 'Beschikbaar', medewerker: 'Geen' },
            { voertuigId: 4, soort: 'Vrachtwagen', merk: 'MAN', type: 'TGX', kleur: 'Rood', kenteken: 'MN-123-OP', aanschafjaar: 2018, status: 'In gebruik', medewerker: 'Kees Klaassen' },
            { voertuigId: 5, soort: 'Auto', merk: 'Toyota', type: 'Yaris', kleur: 'Groen', kenteken: 'QR-456-ST', aanschafjaar: 2022, status: 'Beschikbaar', medewerker: 'Geen' }
        ];
        setVehicles(dummyData);
    }, []);

    const handleSubscriptionChange = (e) => {
        setSubscriptionType(e.target.value);
    };

    return (
        <div style={{ fontFamily: 'Arial, sans-serif' }}>
            <Navbar />
            <h1 style={{ textAlign: 'center' }}>Wagenparkbeheer</h1>

            {/* Tabel met voertuigen */}
            <table style={styles.table}>
                <thead>
                    <tr style={{ backgroundColor: '#f2f2f2' }}>
                        <th style={styles.headerCell}>Voertuig ID</th>
                        <th style={styles.headerCell}>Soort</th>
                        <th style={styles.headerCell}>Merk</th>
                        <th style={styles.headerCell}>Type</th>
                        <th style={styles.headerCell}>Kleur</th>
                        <th style={styles.headerCell}>Kenteken</th>
                        <th style={styles.headerCell}>Aanschafjaar</th>
                        <th style={styles.headerCell}>Status</th>
                        <th style={styles.headerCell}>Medewerker</th>
                    </tr>
                </thead>
                <tbody>
                    {vehicles.map(vehicle => (
                        <tr key={vehicle.voertuigId} style={styles.row}>
                            <td style={styles.cell}>{vehicle.voertuigId}</td>
                            <td style={styles.cell}>{vehicle.soort}</td>
                            <td style={styles.cell}>{vehicle.merk}</td>
                            <td style={styles.cell}>{vehicle.type}</td>
                            <td style={styles.cell}>{vehicle.kleur}</td>
                            <td style={styles.cell}>{vehicle.kenteken}</td>
                            <td style={styles.cell}>{vehicle.aanschafjaar}</td>
                            <td style={{ ...styles.cell, color: getStatusColor(vehicle.status) }}>{vehicle.status}</td>
                            <td style={styles.cell}>{vehicle.medewerker}</td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {/* Tabel met abonnement */}
            <div style={{ marginTop: '40px', textAlign: 'center' }}>
                <h2>Abonnementstype</h2>
                <table style={styles.table}>
                    <thead>
                        <tr style={{ backgroundColor: '#f2f2f2' }}>
                            <th style={styles.headerCell}>Huidig Abonnement</th>
                            <th style={styles.headerCell}>Wijzig Abonnement</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr style={styles.row}>
                            <td style={styles.cell}>{subscriptionType}</td>
                            <td style={styles.cell}>
                                <select value={subscriptionType} onChange={handleSubscriptionChange} style={styles.select}>
                                    <option value="Pay-as-you-go">Pay-as-you-go</option>
                                    <option value="Prepaid">Prepaid</option>
                                </select>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <Footer />
        </div>
    );
};

// Functie om kleur te geven aan de status
const getStatusColor = (status) => {
    switch (status) {
        case 'In gebruik':
            return 'green';
        case 'In reparatie':
            return 'orange';
        case 'Beschikbaar':
            return 'blue';
        default:
            return 'black';
    }
};

// Stijlen
const styles = {
    table: { width: '90%', margin: '20px auto', borderCollapse: 'collapse', borderRadius: '10px', overflow: 'hidden', boxShadow: '0 4px 8px rgba(0,0,0,0.1)' },
    headerCell: { border: '1px solid #ddd', padding: '10px', textAlign: 'left', backgroundColor: '#f2f2f2' },
    cell: { border: '1px solid #ddd', padding: '10px' },
    row: { backgroundColor: '#fff', transition: 'background-color 0.3s' },
    select: { padding: '5px', fontSize: '16px' }
};

export default Temp;
