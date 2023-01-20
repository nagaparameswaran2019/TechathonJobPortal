import React, { useState, useEffect ,createContext, useContext} from 'react';
import { Grid, GridColumn, GridToolbar } from "@progress/kendo-react-grid";
import { ExcelExport } from '@progress/kendo-react-excel-export';
import { GridPDFExport } from "@progress/kendo-react-pdf";
import { getDataService } from '../../services';
import { filterBy } from "@progress/kendo-data-query";
import {LoadingPanel} from "../../SharedControls/loadingpanel"
import dashboardData from './dashboardData.json';

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

const UserContext = createContext();
const initialDataState = {
  skip: 0,
  take: 20,
};
function CollegeDashboard() {
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
    const IsOrgType = sessionStorage.getItem('LoginOrgType') =='INSTITUTION' ? true :false;
    const [orgDesc, setorgDesc] = useState(IsOrgType ? 'College' : 'Company');
    useEffect(() => {
        setTimeout(() => {
        getDataService(apiUrl)
            .then((result) => {
                setDataSource(JSON.parse(JSON.stringify(dashboardData)));
                setcontrolLoaded(true);
            });
        }, 250);
    },[]);

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
  var orgsGrid =[];
  if (Array.isArray(dataSource) && dataSource.length) {
    orgsGrid = (      
        <Grid style={{ height: "600px" }}
        data={filterBy(dataSource, filter)} filterable={true} sortable={true}  filter={filter} onFilterChange={(e) => {setFilter(e.filter);} }
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
                <GridColumn field={IsOrgType ? "CollegeID" :"CompanyID"} title={IsOrgType ? "CollegeID" :"CompanyID"} /> 
                <GridColumn field={IsOrgType ? "CollegeName" :"CompanyName"} title={IsOrgType ? "CollegeName" :"CompanyName"}/>
                <GridColumn field="OrgType" title="OrgType"/>
                {IsOrgType && <GridColumn field="NoOfStudent" title="NoOfStudent" />}
                <GridColumn field="ContactNo" title="ContactNo"/>
                <GridColumn field="EmailID" title="EmailID"/>
            </Grid>
        );
}
    return (
      <div>
      {!controlLoaded && <LoadingPanel/>}                  
      <div className="page-head"><h2>{orgDesc}</h2>
        <ExcelExport data={dataSource} ref={_export} fileName="OrgsGrid.xlsx">
            {orgsGrid}
        </ExcelExport> 
        < GridPDFExport ref={(pdfExport) => (gridPDFExport = pdfExport)} margin="1cm" fileName="OrgsGrid.pdf">
            {orgsGrid}
        </GridPDFExport>             
        </div >
    </div>
    );    
  }export default CollegeDashboard;  
