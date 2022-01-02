import Navbar from "react-bootstrap/Navbar";
import { SignOutButton } from "../SignOutButton";
import { Nav } from "react-bootstrap";
import { useIsAuthenticated } from "@azure/msal-react";
import "./AppNavbar.css"


export const AppNavbar: React.FC = () => {
    const isAuthenticated = useIsAuthenticated();


    return (
        <Navbar bg="primary" variant="dark" expand="lg" className="navbar">
            <Navbar.Brand href="/" className="brand">MiniRent</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                {/* <Nav>
                    <Nav.Link href="#home">Home</Nav.Link>
                    <Nav.Link href="#link">Link</Nav.Link>
                </Nav> */}

                <Nav className="ms-auto">
                    {isAuthenticated && <SignOutButton />}
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    )
}
