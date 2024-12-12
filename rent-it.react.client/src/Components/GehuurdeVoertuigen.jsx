import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Table, Button, Input, Select, DatePicker } from 'antd';
import { saveAs } from 'file-saver';

const { Option } = Select;
const { RangePicker } = DatePicker;

const RentedVehiclesOverview = () => {
    const [vehicles, setVehicles] = useState([]);
    const [filteredVehicles, setFilteredVehicles] = useState([]);
    const [vehicleStatus, setVehicleStatus] = useState('');
    const [vehicleType, setVehicleType] = useState('');
    const [dateRange, setDateRange] = useState([]);

    useEffect(() => {
        fetchVehicles();
    }, []);

    const fetchVehicles = async () => {
        try {
            const response = await axios.get('http://localhost:5000/api/vehicles');
            setVehicles(response.data);
            setFilteredVehicles(response.data);
        } catch (error) {
            console.error('Error fetching vehicles:', error);
        }
    };

    const handleFilterChange = () => {
        let filtered = vehicles;

        if (vehicleStatus) {
            filtered = filtered.filter(vehicle => vehicle.vehicleStatus === vehicleStatus);
        }

        if (vehicleType) {
            filtered = filtered.filter(vehicle => vehicle.vehicleType === vehicleType);
        }

        if (dateRange.length === 2) {
            const [start, end] = dateRange;
            filtered = filtered.filter(vehicle => {
                const rentalDate = new Date(vehicle.rentalStartDate);
                return rentalDate >= start && rentalDate <= end;
            });
        }

        setFilteredVehicles(filtered);
    };

    const handleExport = () => {
        const csvContent = filteredVehicles.map(v =>
            `${v.vehicleId},${v.vehicleType},${v.vehicleModel},${v.vehicleStatus},${v.renterName},${v.rentalStartDate}`
        ).join('\\n');
        const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
        saveAs(blob, 'vehicles.csv');
    };

    const columns = [
        { title: 'ID', dataIndex: 'vehicleId', key: 'vehicleId' },
        { title: 'Type', dataIndex: 'vehicleType', key: 'vehicleType' },
        { title: 'Model', dataIndex: 'vehicleModel', key: 'vehicleModel' },
        { title: 'Status', dataIndex: 'vehicleStatus', key: 'vehicleStatus' },
        { title: 'Huurder', dataIndex: 'renterName', key: 'renterName' },
        { title: 'Startdatum', dataIndex: 'rentalStartDate', key: 'rentalStartDate' },
    ];

    return (
        <><div>
            <h1>Overzicht Verhuurde Voertuigen</h1>
            <div>
                <Select
                    placeholder />\"Status\"
                style={{ width: 200, marginRight: 10 }}
                onChange={value => { setVehicleStatus(value); handleFilterChange(); } }
                >
                <Option value />\"Verhuurd\">Verhuurd</Option>
            <Option value />\"Beschikbaar\">Beschikbaar</Option><Option value /></>"In Reparatie\">In Reparatie</Option>
                </Select >

    <Select
        placeholder=\"Type\"
style = {{ width: 200, marginRight: 10 }}
onChange = { value => { setVehicleType(value); handleFilterChange(); }}
                >
    <Option value=\"Auto\">Auto</Option>
        < Option value =\"Camper\">Camper</Option>
            < Option value =\"Caravan\">Caravan</Option>
                </Select >

                <><RangePicker
                        style={{ marginRight: 10 }}
                        onChange={dates => { setDateRange(dates); handleFilterChange(); } } /><Button onClick={handleExport}>Exporteer CSV</Button></>
            </div >

    <Table dataSource={filteredVehicles} columns={columns} rowKey=\"voertuigId\" />
        </div >
    );
};

export default RentedVehiclesOverview;
