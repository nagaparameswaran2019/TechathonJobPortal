import React, { Suspense, useLayoutEffect, useState, useEffect } from "react";
import { Switch, Route } from "react-router-dom";
import { useForm } from "react-hook-form";
import {
    Form,
    Field,
    FormElement,
    FieldRenderProps,
    FormRenderProps,
} from "@progress/kendo-react-form";
import { Error } from "@progress/kendo-react-labels";
import { Input } from "@progress/kendo-react-inputs";
import { DropDownList } from "@progress/kendo-react-dropdowns";
import { Grid, GridColumn, GridToolbar } from "@progress/kendo-react-grid";
import { ExcelExport } from '@progress/kendo-react-excel-export';
import { GridPDFExport } from "@progress/kendo-react-pdf";
import { getDataService } from '../../services';
import { filterBy } from "@progress/kendo-data-query";
import { DatePicker } from "@progress/kendo-react-dateinputs";
import { LoadingPanel } from "../../SharedControls/loadingpanel";
import { getOrganizationCoreTypes, saveStudentDetails } from '../../services';
import studentData from './StudentData.json';

const initialFilter = {
    logic: "and",
    filters: [
        {
            field: "CollegeID",
            operator: "contains",
            value: "",
        },
    ],
};

const initialDataState = {
    skip: 0,
    take: 20,
};

const StudentDetails = () => {
    const [controlLoaded, setcontrolLoaded] = useState(false);
    const [filter, setFilter] = React.useState(initialFilter);
    const { errors, register, handleSubmit } = useForm();
    let gridPDFExport;
    const _export = React.useRef(null);
    const apiUrl = 'https://localhost:7077/api/Student/GetAllStudentsByOrganizationId/7';
    const [page, setPage] = useState(initialDataState);
    const pageChange = (event) => {
        setPage(event.page);
    };
    const [inputs, setInputs] = useState({});
    const [lookupDataSource, setLookupDataSource] = useState([]);
    const [departmentValue, setDepartmentValue] = useState({});
    const [dataSource, setDataSource] = useState({
        data: []
    });



    useEffect(() => {
        setTimeout(() => {
            getOrganizationCoreTypes('DEPARTMENTTYPE')
                .then((result) => {
                    if (result.isSuccess) {
                        setLookupDataSource(result.data[0].lookUps);
                    }
                    console.log(result);
                });
        }, 100);
    }, []);

    const handleInputChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    }

    const handleFormSubmit = (dataItem) => {
        debugger
        console.log(inputs);
        console.log(departmentValue);
        dataItem['dateOfBirth'] = inputs.dateOfBirth;
        dataItem['departmentId'] = departmentValue.lookUpId;
        dataItem['StatusId'] = 1 /*NOTSTARTED*/;

        dataItem.dateOfBirth = dataItem.dateOfBirth.toLocaleDateString()
        console.log(dataItem);

        setTimeout(() => {
            saveStudentDetails(dataItem)
                .then((result) => {
                    debugger
                    if (result.isSuccess) {
                        
                    }
                    alert(result.message);
                    console.log(result);
                });
        }, 200);

    };
    const handleChange = (event) => {
        setDepartmentValue(event.value);
    };

    const dobHandleChange = (event) => {
        debugger
        // logs.unshift("change: " + event.target.value);
        // setValue(event.target.value);
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    };

    const getAllStudent = () => {
        setcontrolLoaded(false);
        setTimeout(() => {
            getDataService(apiUrl)
                .then((result) => {
                    setDataSource(JSON.parse(JSON.stringify(result.data)));
                    setcontrolLoaded(true);
                });
        }, 250);
    }

    React.useEffect(() => {
        getAllStudent();
    }, []);

    const exportPDF = () => {
        setTimeout(() => {
            if (gridPDFExport) {
                gridPDFExport.save(dataSource);
            }
        }, 250);
    };
    const excelExport = () => {
        if (_export.current !== null) {
            _export.current.save();
        }
    };
    const CustomCell = (props) => {
        const field = props.field || "";
        return (
            <td className="k-command-cell">
                <button className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary k-grid-edit-command" onClick={() => { console.log(props.dataItem[field].toString()); }}>Edit</button>
                <button className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary k-grid-edit-command" onClick={() => getAllStudent()}>Delete</button>
            </td>
        );
    };
    const MyCustomCell = (props) => <CustomCell {...props} />;
    var studentGrid = [];
    if (Array.isArray(dataSource) && dataSource.length) {
        studentGrid = (
            <Grid style={{ height: "600px" }}
                data={filterBy(dataSource, filter)} filterable={false} sortable={false} filter={filter} onFilterChange={(e) => { setFilter(e.filter); }}
                total={dataSource.length}
            >
                <GridToolbar>
                    <button
                        title="Export Excel"
                        className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary"
                        onClick={excelExport}
                    >
                        Export to Excel
                    </button>
                    <button
                        title="Export PDF"
                        className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary"
                        onClick={exportPDF}>
                        Export PDF
                    </button>
                </GridToolbar>
                <br></br>
                <br></br>
                <GridColumn field="studentId" title="StudentID" />
                <GridColumn field="firstName" title="FirstName" />
                <GridColumn field="lastName" title="LastName" />
                <GridColumn field="departmentName" title="DepartmentName" />
                <GridColumn field="department.departmentId" title="DepartmentID" />
                <GridColumn field="dateOfBirth" title="DateofBirth" />
                <GridColumn field="cgpaorPercentage" title="CGPAOrPercentage" />
                <GridColumn field="email" title="EmailID" />
                <GridColumn field="contact" title="Contact" />
                <GridColumn cell={MyCustomCell} field="studentID" title=" " width="200px" />
            </Grid>
        );
    }
    useLayoutEffect(() => {
        window.scrollTo(0, 0);
    });
    const emailRegex = new RegExp(/\S+@\S+\.\S+/);
    const emailValidator = (value) =>
        emailRegex.test(value) ? "" : "Please enter a valid email.";
    const EmailInput = (fieldRenderProps) => {
        const { validationMessage, visited, ...others } = fieldRenderProps;
        return (
            <div>
                <Input {...others} />
                {visited && validationMessage && <Error>{validationMessage}</Error>}
            </div>
        );
    };

    return (
        <div style={{ marginTop: 65, position: "fixed" }} className="col-md-4">
            <h4>Student Details</h4>
            <form className="is-alter" onSubmit={handleSubmit(handleFormSubmit)}>
                <div className="form-group" style={{ marginBottom: 15 }}>
                    <label className="form-label" htmlFor="firstName">
                        First Name</label>
                    <div className="form-control-wrap">
                        <input
                            type="text"
                            id="firstName"
                            name="firstName"
                            value={inputs.firstName || ""}
                            onChange={handleInputChange}
                            placeholder="First name"
                            ref={register({ required: true })}
                            className="form-control-lg form-control"
                        />
                    </div>
                </div>
                <div className="form-group" style={{ marginBottom: 15 }}>
                    <label className="form-label" htmlFor="lastName">
                        Last Name</label>
                    {/* "studentId": 0,
  "firstName": "Mani",
  "lastName": "Kumar",
  "dateOfBirth": "10/10/1990",
  "email": "test@email.com",
  "cgpaorPercentage": 5.6,
  "contact": "12345",
  "statusId": 1,
  "departmentId": 8 */}
                    <div className="form-control-wrap">
                        <input
                            type="text"
                            id="lastName"
                            name="lastName"
                            value={inputs.lastName || ""}
                            onChange={handleInputChange}
                            placeholder="Last name"
                            ref={register({ required: true })}
                            className="form-control-lg form-control"
                        />
                    </div>
                </div>
                <div className="form-group" style={{ marginBottom: 15 }}>
                    <label className="form-label" htmlFor="email">
                        Email</label>
                    <div className="form-control-wrap">
                        <input
                            type="email"
                            id="email"
                            name="email"
                            value={inputs.email || ""}
                            ref={register({ required: true })}
                            onChange={handleInputChange}
                            className="form-control-lg form-control"
                            placeholder="Email"
                        />
                    </div>
                </div>
                <div className="form-group" style={{ marginBottom: 15 }}>
                    <label className="form-label" htmlFor="dateOfBirth">
                        DOB</label>
                    <div className="form-control-wrap">
                        <DatePicker
                            name="dateOfBirth"
                            id="dateOfBirth"
                            format="MM/dd/yyyy"
                            onChange={dobHandleChange}
                            value={inputs.dateOfBirth || ""}
                        />
                        {/* <input
                            type="text"
                            id="dateOfBirth"
                            name="dateOfBirth"
                            value={inputs.dateOfBirth || ""}
                            ref={register({ required: true })}
                            onChange={handleInputChange}
                            className="form-control-lg form-control"
                            placeholder="Date Of Birth"
                        /> */}
                    </div>
                </div>
                <div className="form-group" style={{ marginBottom: 15 }}>
                    <label className="form-label" htmlFor="cgpaorPercentage">
                        CGPA/Percentage</label>
                    <div className="form-control-wrap">
                        <input
                            type="text"
                            id="cgpaorPercentage"
                            name="cgpaorPercentage"
                            value={inputs.cgpaorPercentage || ""}
                            ref={register({ required: true })}
                            onChange={handleInputChange}
                            className="form-control-lg form-control"
                            placeholder="CGPA/Percentage"
                        />
                    </div>
                </div>
                <div className="form-group" style={{ marginBottom: 15 }}>
                    <label className="form-label" htmlFor="contact">
                        Contact</label>
                    <div className="form-control-wrap">
                        <input
                            type="text"
                            id="contact"
                            name="contact"
                            value={inputs.contact || ""}
                            ref={register({ required: true })}
                            onChange={handleInputChange}
                            className="form-control-lg form-control"
                            placeholder="Contact"
                        />
                    </div>
                </div>
                <div className="form-group" style={{ marginBottom: 15 }}>
                    <label className="form-label" htmlFor="name">
                        Department</label>
                    <div className="form-control-wrap">
                        <DropDownList
                            data={lookupDataSource}
                            textField="description"
                            dataItemKey="lookUpId"
                            value={departmentValue}
                            name="department"
                            onChange={handleChange}
                            placeholder="Please select ..."
                        />
                    </div>
                </div>
                <div className="k-form-buttons">
                    <button
                        type={"submit"}
                        className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-base">
                        Submit
                    </button>
                </div>

            </form>

            <br></br>
            <br></br>
            <div style={{ position: "fixed" }} className="col-md-8">
                {!controlLoaded && <LoadingPanel />}
                <div className="page-head"><h2>Student List</h2>
                    <ExcelExport data={dataSource} ref={_export} fileName="studentGrid.xlsx">
                        {studentGrid}
                    </ExcelExport>
                    < GridPDFExport ref={(pdfExport) => (gridPDFExport = pdfExport)} margin="1cm" fileName="studentGrid.pdf">
                        {studentGrid}
                    </GridPDFExport>
                </div >
            </div>
        </div>
    );
};
export default StudentDetails;
