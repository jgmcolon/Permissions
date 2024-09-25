import {Outlet} from "react-router-dom";
import Footer from "../../components/share/footer";
import Header from "../../components/share/header"
import {Helmet, HelmetProvider} from "react-helmet-async";
import {CONFIG} from "../../config-global.jsx";


export default function LayoutAdmin() {
    return <>
        <HelmetProvider>
            <Helmet>
                <title> {`Home Page | ${CONFIG.appName}`}</title>
            </Helmet>
        </HelmetProvider>


        <Header/>
        <main role="main" >
            <Outlet/>
        </main>
        <Footer/>
    </>

}
