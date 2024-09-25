import {Button, CardActions, CardContent, FormControl, InputLabel, MenuItem, Select} from "@mui/material";
import Typography from "@mui/material/Typography";
import {GetBy, Modify, Request} from "../../services/permission"
import {List} from "../../services/permissionType";
import {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import {Field, Form, Formik} from "formik";
import dayjs from 'dayjs';
import Grid from "@mui/material/Grid2";
import {TextField} from "formik-material-ui";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {DatePicker} from "@mui/x-date-pickers/DatePicker";
import * as Yup from "yup";
import moment from 'moment';

export default function PermissionModifyPage() {
    const {Id} = useParams();
    const navigate = useNavigate();
    const [permisionTypeList, setPermisionTypeList] = useState([]);
    const [row, setRow] = useState({});
    const [loading, setLoading] = useState(false);

    const [edit, setEdit] = useState(false);

    useEffect(() => {
        fetchData()
    }, [])



    const fetchData = async () => {
        setLoading(true);
        await GetBy(
            Id,
            (data, ok) => {
                if (ok) {
                    setRow(data || {});


                }

                console.log("LOADED DATA");
                setLoading(false);
            }
        )

        await List(
            (data, ok) => {
                if (ok) {
                    setPermisionTypeList(data || []);
                }
                setLoading(false);
            }
        )
    };

    const ViewPermision =  () =>
        <>
            <Typography variant="h5" component="div">
                Employee  {row.employeeName} {row.employeeLastName}
            </Typography>
            <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                Permision <strong>{row?.permissionType}</strong>
            </Typography>
            <Typography variant="body2">
                Date {row.permissionDate}
            </Typography>
        </>

    const EditPermision = ({item}) => {
        let validationSchema = Yup.object().shape({
            firstName: Yup.string().required("Required"),
            lastName: Yup.string().required("Required"),
            permisionType: Yup.string().required("Required"),
            permisionDate: Yup.date().required("Required"),
        })


        let initialValues = {
            firstName: item?.employeeName,
            lastName: item?.employeeLastName,
            permisionType: item?.permissionTypeId,
            permisionDate: dayjs(item?.permissionDate)
        }


        const onSubmit = (values) => {

            var permisionDate = values.permisionDate.$d.toISOString().substring(0, 10);

            let dto = {
                employeeName: values.firstName,
                employeeLastName: values.lastName,
                permissionTypeId: values.permisionType,
                permissionDate: permisionDate
            };

            console.log(dto)

            setLoading(true);

            Modify(Id, dto, (data, ok) => {
                if (ok) {
                    navigate(`/modify/${Id}`);
                }
                setLoading(false);
            })

        }

      return   <>
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
                                        <LocalizationProvider dateAdapter={AdapterDayjs} >
                                            <DatePicker disablePast
                                                        onChange={(value) =>{
                                                            setFieldValue("permisionDate", value, true);
                                                        }}
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
                                    Update
                                </Button>
                            </CardActions>
                        </Form>
                    )
                }}
            </Formik>
        </>
    }

    return (
        <>
            <CardContent>
                {edit ? <EditPermision item={row} />: <ViewPermision/>}
            </CardContent>
            <CardActions>
                { !edit ?
                <Button variant="contained" size="large"  onClick={() => {setEdit(!edit)}}>Edit</Button> : <></>}
            </CardActions>
        </>
    );
}
