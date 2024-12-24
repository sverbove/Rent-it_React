import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import Swal from "sweetalert2";

const ProtectedRoute = ({ allowedRoles }) => {
    const token = localStorage.getItem("token");

    // Check if token exists
    if (!token) {
        Swal.fire({
            icon: "warning",
            title: "Niet ingelogd",
            text: "Je moet ingelogd zijn om deze pagina te bekijken.",
        });
        return <Navigate to="/login" replace />;
    }

    let user = null;

    // Decode token
    try {
        user = jwtDecode(token);
    } catch (error) {
        console.error("Invalid token:", error.message);
        localStorage.removeItem("token"); // Remove invalid token
        Swal.fire({
            icon: "error",
            title: "Ongeldig Token",
            text: "Je sessie is ongeldig. Log opnieuw in.",
        });
        return <Navigate to="/login" replace />;
    }

    // Validate user role
    if (!allowedRoles.includes(user.role)) {
        Swal.fire({
            icon: "error",
            title: "Geen Toegang",
            text: `Je moet een ${allowedRoles.join(" of ")} zijn om deze pagina te bekijken.`,
        });
        return <Navigate to="/unauthorized" replace />;
    }

    return <Outlet />;
};

export default ProtectedRoute;