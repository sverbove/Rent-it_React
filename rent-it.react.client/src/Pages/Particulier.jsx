import Navbar from '/src/Components/Navbar';
import Footer from '/src/Components/Footer';
import { Link } from 'react-router-dom';
import '/src/css/Particulier.css';

const Particulier = () => {
    const catalogusItems = [
        {
            title: "Auto's",
            description: "Praktische en zuinige voertuigen voor dagelijks gebruik en korte trips.",
            image: "/src/assets/auto.png",
        },
        {
            title: "Campers",
            description: "Comfortabel reizen met alle voorzieningen, ideaal voor vakanties.",
            image: "/src/assets/camper.jpg",
        },
        {
            title: "Caravans",
            description: "Perfect voor langere verblijven op vakantie met veel ruimte.",
            image: "/src/assets/caravan.jpg",
        },
    ];

    const scrollToCatalog = () => {
        document.querySelector('.catalogusSection').scrollIntoView({ behavior: 'smooth' });
    };

    return (
        <div className="mainWrapper fade-in">
            <Navbar />
            <div className="particulierHero">
                <h1>Ontdek onze voertuigen!</h1>
                <p>Van stadsritten tot vakantieavonturen, wij hebben het perfecte voertuig voor jou.</p>
                <button className="ctaButton" onClick={scrollToCatalog}>Bekijk voertuigen</button>
            </div>
            <section className="catalogusSection">
                <h2>Voertuigcategorieen</h2>
                <div className="catalogusGrid">
                    {catalogusItems.map((item, index) => (
                        <div
                            key={index}
                            className="catalogusCard"
                        >
                            <img src={item.image} alt={item.title} />
                            <div className="catalogusContent">
                                <h3>{item.title}</h3>
                                <p>{item.description}</p>
                                <button>Bekijk Details</button>
                            </div>
                        </div>
                    ))}
                </div>

                {/* Reserveer Voertuig Knop */}
                <div className="reserveerWrapper">
                    <Link to="/aanvraag">
                        <button className="reserveerButton">Reserveer je voertuig</button>
                    </Link>
                </div>
            </section>
            <Footer />
        </div>
    );
};

export default Particulier;
