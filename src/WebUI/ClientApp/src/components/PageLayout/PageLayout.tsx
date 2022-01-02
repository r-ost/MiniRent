import { Outlet } from "react-router-dom";
import { AppNavbar } from "../AppNavbar/AppNavbar";


export const PageLayout = () => {
    return (
        <>
            <AppNavbar></AppNavbar>
            <Outlet></Outlet>
        </>
    );
};