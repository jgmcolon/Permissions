import Box from '@mui/material/Box';
import { DataGrid } from '@mui/x-data-grid';


const columns = [
    { field: 'id', headerName: 'Id', width: 90 },
    {
        field: 'description',
        headerName: 'Description',
        sortable: false,
        minWidth:350,

    }
];


export default function PermissionTypesList({data}) {

    return <Box sx={{ height: 400, width: '100%' }}>
        <DataGrid
            rows={data}
            columns={columns}
            initialState={{
                pagination: {
                    paginationModel: {
                        pageSize: 5,
                    },
                },
            }}
            pageSizeOptions={[5]}
            checkboxSelection
            disableRowSelectionOnClick
        />
    </Box>
}
