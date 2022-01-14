import Navbar from "react-bootstrap/Navbar";
import { SignOutButton } from "../SignOutButton";
import { Button, Nav } from "react-bootstrap";
import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import "./AppNavbar.css"
import { useNavigate } from "react-router-dom";


export const AppNavbar: React.FC = () => {
    const isAuthenticated = useIsAuthenticated();
    let navigate = useNavigate();

    const { accounts } = useMsal();

    const name = accounts[0] && accounts[0].username;

    return (
        <Navbar bg="primary" variant="dark" expand="lg" className="navbar">
            <Navbar.Brand href="/" className="brand">MiniRent</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="ms-auto flex items-center">
                    <div className="text-gray-200 mr-2">{name}</div>
                    {isAuthenticated && <Button variant="secondary" className="ml-auto mr-2" onClick={() => navigate("/account")}>My account</Button>}
                    {isAuthenticated && <SignOutButton />}
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    )
}
