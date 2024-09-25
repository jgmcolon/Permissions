import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import {redirect} from "react-router-dom";
import {Link} from "@mui/material";


const pages = [ {path: "request", name:"Request"},
                                           {path: "list", name:"List"}

                        ];


export default function Header() {



    const [anchorElNav, setAnchorElNav] = React.useState(null);


    const handleOpenNavMenu = (event) => {
        setAnchorElNav(event.currentTarget);
    };

    const handleCloseNavMenu = (path) => {
        redirect(path);
        console.log(path);
    };



    return (
        <AppBar position="static">
            <Container maxWidth="xl">
                <Toolbar disableGutters>
                    <Box sx={{display: {xs: 'none', md: 'flex'}, mr: 1}}>
                        <img src="/logo.svg" alt="logo"/>
                    </Box>


                    <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
                        <IconButton
                            size="large"
                            aria-label="account of current user"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            onClick={handleOpenNavMenu}
                            color="inherit"
                        >
                            <MenuIcon />
                        </IconButton>
                        {pages.map((page, inx) => (
                           <>
                            <Link href={page.path} color="#fff" underline="always">
                                {page.name}
                            </Link> |
                           </>
                        ))}
                    </Box>
                    <Box sx={{display: {xs: 'flex', md: 'none'}, mr: 1}}>
                        <img src="/logo.svg" alt="logo"/>
                    </Box>
                    <Typography
                        variant="h5"
                        noWrap
                        component="a"
                        href="#app-bar-with-responsive-menu"
                        sx={{
                            mr: 2,
                            display: { xs: 'flex', md: 'none' },
                            flexGrow: 1,
                            fontFamily: 'monospace',
                            fontWeight: 700,
                            letterSpacing: '.3rem',
                            color: 'inherit',
                            textDecoration: 'none',
                        }}
                    >
                        Permissions
                    </Typography>
                    <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                        {pages.map((page, inx) => (
                            <>
                                <Link href={page.path} color="#fff" underline="always">
                                    {page.name}
                                </Link> |
                            </>
                        ))}
                    </Box>
                    <Box sx={{ flexGrow: 0 }}>
                        <Tooltip title="Open settings">
                            <Avatar alt="Remy Sharp" src="/user.jpg" />
                       </Tooltip>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    )
}
