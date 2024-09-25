import PermissionTypesList from "../../components/forms/permissionTypes/index.jsx";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";



export default function PermissionTypePage() {
    return (
        <>
            <Box sx={{
                display: 'flex',
                borderRadius: 1,
            }}>
                <Typography  sx={{ flexGrow: 1}} id="modal-modal-title" variant="h6" component="h2">
                    Permission Type Page
                </Typography>

                <Button sx={{ flexGrow: 0}} variant="outlined">Add</Button>
            </Box>


            <PermissionTypesList/>
        </>
    );
}
