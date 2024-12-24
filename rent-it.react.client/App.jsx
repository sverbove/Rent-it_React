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
import PrivacyOvereenkomst from "/src/Pages/PrivacyOvereenkomst";
import LoginForm from "/src/Components/LoginForm";
import Aanvraag from "/src/Pages/Aanvraag";
import Temp from "/src/Pages/Temp";

const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/Home" element={<Home />} />
                <Route path="/Zakelijk" element={<Zakelijk />} />
                <Route path="/Particulier" element={<Particulier />} />
                <Route path="/Info" element={<Info />} />
                <Route path="/Contact" element={<Contact />} />
                <Route path="/LogIn" element={<LogIn />} />
                <Route path="/Register" element={<Register />} />
                <Route path="/AbonnementenInfo" element={<AbonnementenInfo />} />
                <Route path="/AlgemeneVoorwaarden" element={<AlgemeneVoorwaarden />} />
                <Route path="/PrivacyOvereenkomst" element={<PrivacyOvereenkomst />} />
                <Route path="/login" element={<LoginForm />} />
                <Route path="/home" element={<Home />} />
                <Route path="/aanvraag" element={<Aanvraag />} />
                <Route path="/Temp" element={<Temp />} />
            </Routes>
        </BrowserRouter>
    );
};

export default App;
