import Typography from "@mui/material/Typography";

import Container from "@mui/material/Container";
import {Link} from "@mui/material";
import Box from "@mui/material/Box";

export default function PageNotFound() {
    return (
        <Container style={{textAlign: 'center'}}>
            <Typography variant="h3" sx={{mb: 2}}>
                Sorry, page not found!
            </Typography>

            <Typography sx={{color: 'text.secondary'}}>
                Sorry, we couldn’t find the page you’re looking for. Perhaps you’ve mistyped the URL? Be
                sure to check your spelling.
            </Typography>

            <Box
                component="img"
                src="/illustration-404.svg"

            />

            <Link href="/" size="large" variant="contained" color="inherit">
                Go to home
            </Link>
        </Container>
    )
}
