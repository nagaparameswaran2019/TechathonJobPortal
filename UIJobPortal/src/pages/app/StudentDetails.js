import React, { Suspense, useLayoutEffect,useState, useEffect } from "react";
import { Switch, Route } from "react-router-dom";
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
import {LoadingPanel} from "../../SharedControls/loadingpanel"
import studentData from './StudentData.json';

const initialFilter = {
    logic: "and",
    filters:[
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
let gridPDFExport;
const _export = React.useRef(null);
const apiUrl = 'https://api.github.com/users/hadley/orgs';
const [page, setPage] = useState(initialDataState);
const pageChange = (event) => {
    setPage(event.page);
};
const [dataSource, setDataSource] = useState({
    data: []
});
const getAllStudent = () => {
    setcontrolLoaded(false);
    debugger;
    setTimeout(() => {
    getDataService(apiUrl)
        .then((result) => {
            setDataSource(JSON.parse(JSON.stringify(studentData)));
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
    <button className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary k-grid-edit-command" onClick={() => { console.log(props.dataItem[field].toString());}}>Edit</button> 
    <button className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary k-grid-edit-command" onClick={() => getAllStudent()}>Delete</button> 
  </td>
);
};
const MyCustomCell = (props) => <CustomCell {...props}  />;
var studentGrid =[];
if (Array.isArray(dataSource) && dataSource.length) {
    studentGrid = (      
    <Grid style={{ height: "600px" }}   
    data={filterBy(dataSource, filter)} filterable={false} sortable={false}  filter={filter} onFilterChange={(e) => {setFilter(e.filter);} }
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
            <GridColumn field="studentID" title="StudentID" /> 
            <GridColumn field="firstName" title="FirstName" />
            <GridColumn field="lastName" title="LastName"/>
            <GridColumn field="dateofbirth" title="DateofBirth" /> 
            <GridColumn field="cgpaorpercentage" title="CGPAOrPercentage"/>
            <GridColumn field="email" title="EmailID" />       
            <GridColumn field="contact" title="Contact" />         
            <GridColumn cell={MyCustomCell} field="studentID"  title=" "  width="200px"  />            
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
    const sizes = ["X-Small", "Small", "Medium", "Large", "X-Large", "2X-Large"];
    const handleSubmit = (dataItem) => alert(JSON.stringify(dataItem, null, 2));
    return (
        <div style={{ marginTop: 65, position: "fixed" }} class="col-md-4">
            <h4>Student Details</h4>
            <Form style={{ outerWidth: 500 }}
                onSubmit={handleSubmit}
                render={(formRenderProps) => (
                    <FormElement>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                First Name</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"firstName"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Last Name</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"lastName"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Email</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"email"}
                                    type={"email"}
                                    component={EmailInput}
                                    validator={emailValidator}
                                />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                DateOfBirth</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"dateofbirth"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                CGPA/Percentage</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"cgpaorpercentage"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Contact</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"contact"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Department</label>
                            <div class="form-control-wrap">
                                <DropDownList
                                    data={sizes} 
                                    style={{
                                        width: "300px",
                                    }}
                                />
                            </div>
                        </div>


                        <div className="k-form-buttons">
                            <button
                                type={"submit"}
                                className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-base"
                                disabled={!formRenderProps.allowSubmit}
                            >
                                Submit
                            </button>
                        </div>
                    </FormElement>
                )}
            />
            <br></br>
            <br></br>
            <div style={{ position: "fixed"}} class="col-md-8"> 
            {!controlLoaded && <LoadingPanel/>}                  
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
