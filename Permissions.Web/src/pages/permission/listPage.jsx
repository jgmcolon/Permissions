import {DataGrid} from "@mui/x-data-grid";
import Box from "@mui/material/Box";
import {useEffect, useState} from "react";
import {GetAll} from "../../services/permission"

const columns = [
    { field: 'id', headerName: 'Id', width: 90 },
    {
        field: 'fullName',
        headerName: 'Full name',
        sortable: false,
        minWidth:350,
        valueGetter: (value, row) => `${row.employeeName || ''} ${row.employeeLastName || ''}`,
    },
    {
        field: 'permissionType',
        headerName: 'Permission Type',
        type: 'number',
        minWidth:200,
    },
    {
        field: 'permissionDate',
        headerName: 'Permission Date',
        type: 'number',
        minWidth:200,
    },
];



export default function PermissionListPage() {

    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [totalRows, setTotalRows] = useState(0);

    const [paginationModel, setPaginationModel] = useState({
        pageSize: 5,
        page: 0,
    });

    useEffect(() => {
            fetchData(paginationModel.page)
    }, [paginationModel])


    const fetchData = async (page) => {
        setLoading(true);

        await GetAll(
            page,
            paginationModel.pageSize,
            (data, ok) => {
                if (ok) {
                    setData(data?.list || []);
                    setTotalRows(data?.total || 0);
                }

                console.log("LOADED DATA");
                setLoading(false);
            }
        )
    };


    return (

        <Box sx={{ minHeight: 500, width: '100%' }}>
            <DataGrid
                rows={data}
                columns={columns}
                paginationModel={paginationModel}
                onPaginationModelChange={setPaginationModel}
                initialState={{
                    pagination: {
                        paginationModel: {
                            pageSize: paginationModel.pageSize,
                        },
                    },
                }}
                pageSizeOptions={[5, 10, 25,50]}
                loading={loading}
                rowCount={totalRows}
                paginationMode="server"
                disableRowSelectionOnClick
            />
        </Box>
    );
}
