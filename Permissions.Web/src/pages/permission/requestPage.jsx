import {useEffect, useState} from "react";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs';

import {DatePicker} from '@mui/x-date-pickers/DatePicker';
import {
    Box,
    Card,
    CardContent,
    MenuItem,
    InputLabel,
    Select,
    CardActions,
    Button,
    CardHeader,
    FormControl,
} from "@mui/material"

import Grid from '@mui/material/Grid2';
import moment from 'moment';

import {Formik, Form, Field} from "formik"
import * as Yup from "yup"
import {TextField} from "formik-material-ui"
import {List} from "../../services/permissionType";
import {Request} from "../../services/permission"
import {useNavigate} from "react-router-dom";

export default function PermissionRequestPage() {

    const navigate = useNavigate();
    const [permisionTypeList, setPermisionTypeList] = useState([]);
    const [loading, setLoading] = useState(false);

    const initialValues = {
        firstName: "",
        lastName: "",
        permisionType: "",
        permisionDate: null,

    }

    useEffect(() => {
        console.log("LOAD DATA");
        fetchData()
    }, [])

    const fetchData = async () => {
        setLoading(true);

        await List(
            (data, ok) => {
                if (ok) {
                    setPermisionTypeList(data || []);
                }
                setLoading(false);
            }
        )
    };

    const onSubmit = (values) => {
        console.log(values.permisionDate);

        let dto = {
            employeeName: values.firstName,
            employeeLastName: values.lastName,
            permissionTypeId: values.permisionType,
            permissionDate: moment(values.permisionDate).format('YYYY-MM-DD')
        }
        setLoading(true);

        Request(dto, (data, ok) => {
            if (ok) {
                navigate(`/modify/${data}`);
            }
            setLoading(false);
        })

    }

    let validationSchema = Yup.object().shape({
        firstName: Yup.string().required("Required"),
        lastName: Yup.string().required("Required"),
        permisionType: Yup.string().required("Required"),
        permisionDate: Yup.date().required("Required"),
    })


    return (

        <Box sx={{flexGrow: 1}}>
            <Grid container spacing={2}>
                <Card>
                    <CardHeader title="Request Permision"></CardHeader>
                    <Formik
                        initialValues={initialValues}
                        validationSchema={validationSchema}
                        onSubmit={onSubmit}>
                        {({dirty, isValid, values, handleChange, handleBlur, setFieldValue}) => {
                            return (
                                <Form>
                                    <CardContent>
                                        <Grid item container spacing={1} justify="center">
                                            <Grid size={{xs: 12, sm: 8, md: 6}}>
                                                <Field
                                                    label="First Name"
                                                    variant="outlined"
                                                    fullWidth
                                                    name="firstName"
                                                    value={values.firstName}
                                                    component={TextField}
                                                />
                                            </Grid>
                                            <Grid size={{xs: 12, sm: 8, md: 6}}>
                                                <Field
                                                    label="Last Name"
                                                    variant="outlined"
                                                    fullWidth
                                                    name="lastName"
                                                    value={values.lastName}
                                                    component={TextField}
                                                />
                                            </Grid>

                                            <Grid size={{xs: 12, sm: 8, md: 6}}>
                                                <FormControl fullWidth variant="outlined">
                                                    <InputLabel id="permisionType-label">
                                                        Permision Type
                                                    </InputLabel>
                                                    <Select
                                                        labelId="permisionType-label"
                                                        label="Permision Type"
                                                        onChange={handleChange}
                                                        onBlur={handleBlur}
                                                        value={values.permisionType}
                                                        name="permisionType">
                                                        {permisionTypeList.map((option) => (
                                                            <MenuItem key={option.id} value={option.id}>
                                                                {option.name}
                                                            </MenuItem>
                                                        ))}
                                                    </Select>
                                                </FormControl>
                                            </Grid>
                                            <Grid size={{xs: 12, sm: 8, md: 6}}>
                                                <LocalizationProvider dateAdapter={AdapterDayjs}>
                                                    <DatePicker disablePast
                                                                onChange={(value) => setFieldValue("permisionDate", value, true)}
                                                                value={values.permisionDate} name="permisionDate"
                                                                label="Permision Date"/>
                                                </LocalizationProvider>
                                            </Grid>
                                        </Grid>
                                    </CardContent>
                                    <CardActions>
                                        <Button
                                            disabled={!dirty || !isValid}
                                            variant="contained"
                                            color="primary"
                                            type="Submit">
                                            REGISTER
                                        </Button>
                                    </CardActions>
                                </Form>
                            )
                        }}
                    </Formik>
                </Card>
            </Grid>
        </Box>
    );
}
