import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "/src/Pages/Home";
import Zakelijk from "/src/Pages/Zakelijk";
import Particulier from "/src/Pages/Particulier";
import Catalogus from "/src/Pages/Catalogus";
import Contact from "/src/Pages/Contact";
import LogIn from "/src/Pages/LogIn";
import Register from "/src/Pages/Register";
import AbonnementenInfo from "/src/Pages/AbonnementenInfo";
import LoginForm from "./src/Components/LoginForm";

const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/Home" element={<Home />} />
                <Route path="/Zakelijk" element={<Zakelijk />} />
                <Route path="/Particulier" element={<Particulier />} />
                <Route path="/Catalogus" element={<Catalogus />} />
                <Route path="/Contact" element={<Contact />} />
                <Route path="/LogIn" element={<LogIn />} />
                <Route path="/Register" element={<Register />} />
                <Route path="/AbonnementenInfo" element={<AbonnementenInfo />} />

                <Route path="/login" element={<LoginForm />} />
                <Route path="/home" element={<Home />} />
            </Routes>
        </BrowserRouter>
    );
};

export default App;