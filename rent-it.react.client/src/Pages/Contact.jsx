import React from 'react';
import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import Swal from 'sweetalert2';
import '/src/css/Contact.css';

const Contact = () => {

    const onSubmit = async (event) => {
        event.preventDefault();
        const formData = new FormData(event.target);

        formData.append("access_key", "cf75fc12-881a-465d-a542-ecbc9303644b");

        const object = Object.fromEntries(formData);
        const json = JSON.stringify(object);

        const res = await fetch("https://api.web3forms.com/submit", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json"
            },
            body: json
        }).then((res) => res.json());

        if (res.success) {
            Swal.fire({
                title: "Gelukt!",
                text: "We hebben je bericht ontvangen!",
                icon: "success"
            });
        }
    };

    return (
        <div className="mainWrapper">
            <Navbar />
            <section className="contact">
                <form onSubmit={onSubmit} className="contactForm">
                    <h2>Contact Formulier</h2>
                    <div className="input-box">
                        <label className="hiddenLabel">Volledige naam</label>
                        <input type="text" className="field" placeholder='Vul uw naam in' name='name' required />
                    </div>
                    <div className="input-box">
                        <label className="hiddenLabel">Email-adres</label>
                        <input type="text" className="field" placeholder='Vul uw email-adres in' name='email' required />
                    </div>
                    <div className="input-box">
                        <label className="hiddenLabel">Bericht</label>
                        <textarea name="message" className="field mess" placeholder='Vul uw bericht in' required></textarea>
                    </div>
                    <div className="button-box">
                        <button type="submit">Verstuur bericht</button>
                    </div>
                </form>
            </section>
            <Footer />
        </div>
    );
}

export default Contact;
