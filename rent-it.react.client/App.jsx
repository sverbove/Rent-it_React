import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "/src/Pages/Home";
import Zakelijk from "/src/Pages/Zakelijk";
import Particulier from "/src/Pages/Particulier";
import Info from "/src/Pages/Info";
import Contact from "/src/Pages/Contact";
import LogIn from "/src/Pages/LogIn";
import Register from "/src/Pages/Register";
import AbonnementenInfo from "/src/Pages/AbonnementenInfo";
import AlgemeneVoorwaarden from "/src/Pages/AlgemeneVoorwaarden";
import PrivacyOvereenkomst from "/src/Pages/PrivacyOvereenkomst"
import LoginForm from "./src/Components/LoginForm";
import ProtectedRoute from "./src/Components/ProtectedRoute";
import AddMedewerkerForm from "./src/Components/AddMedewerkerForm";

const Unauthorized = () => (
    <div>
        <h1>Unauthorized Access</h1>
        <p>Je hebt geen permissie om deze pagina te bekijken.</p>
    </div>
)

const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                { /* Public Routes */ }
                <Route path="/" element={<Home /> } />
                <Route path="/Home" element={<Home />} />
                <Route path="/Zakelijk" element={<Zakelijk />} />
                
                <Route path="/Info" element={<Info />} />
                <Route path="/Contact" element={<Contact />} />
                <Route path="/LogIn" element={<LogIn />} />
                <Route path="/Register" element={<Register />} />
                <Route path="/AlgemeneVoorwaarden" element={<AlgemeneVoorwaarden />} />
                <Route path="/PrivacyOvereenkomst" element={<PrivacyOvereenkomst />} />
                <Route path="/login" element={<LoginForm />} />
                <Route path="/unauthorized" element={<Unauthorized />} />

                {/* Protected Routes "Particuliere Klant" */}
                <Route element={<ProtectedRoute allowedRoles={["Particuliere Klant"]} />}>
                    <Route path="/Particulier" element={<Particulier />} />
                </Route>

                {/* Protected Routes "Zakelijke Klant" */}
                <Route element={<ProtectedRoute allowedRoles={["Zakelijke Klant"]} />}>
                    <Route path="/AbonnementenInfo" element={<AbonnementenInfo />} />
                    <Route path="/add-medewerker" element={<AddMedewerkerForm />} />
                </Route>
            </Routes>
        </BrowserRouter> 
    );
};

export default App;