import { Outlet } from "react-router-dom";
import { AppNavbar } from "../AppNavbar/AppNavbar";


export const PageLayout = () => {
    return (
        <div className="flex flex-col h-screen ">
            <div >
                <AppNavbar></AppNavbar>
            </div>
            <div className="grow">
                <Outlet></Outlet>
            </div>
            <div></div>
        </div>
    );
};