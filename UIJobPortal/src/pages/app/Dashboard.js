import React, { useState, useEffect } from 'react';
import { Grid, GridColumn, GridToolbar } from "@progress/kendo-react-grid";
import { ExcelExport } from '@progress/kendo-react-excel-export';
import { GridPDFExport } from "@progress/kendo-react-pdf";
import { getDataService, getAllOrganization } from '../../services';
import { filterBy } from "@progress/kendo-data-query";
import { LoadingPanel } from "../../SharedControls/loadingpanel"
import dashboardData from './dashboardData.json';

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
function Dashboard() {
  const [controlLoaded, setcontrolLoaded] = useState(false);
  const [filter, setFilter] = React.useState(initialFilter);
  const [buttonDesc, setbuttonDesc] = useState('');
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
  const IsOrgType = sessionStorage.getItem('LoginOrgType') == 'INSTITUTION' ? true : false;
  const [orgDesc, setorgDesc] = useState(IsOrgType ? 'College' : 'Company');
  var record = JSON.parse(localStorage.userData);
  useEffect(() => {
    setTimeout(() => {
      getAllOrganization(apiUrl)
        .then((result) => {
          debugger
          var organizationTypeId = record.organizationType == "INSTITUTION" ? 10 : 11;
          setorgDesc(organizationTypeId == 10 ? 'Companies' : 'Institutions');
          setbuttonDesc(organizationTypeId == 10 ? 'Reply' : 'Invite')
          var dataSource = result.data.filter(r => r.organizationTypeId == organizationTypeId);
          console.log(dataSource);
          setDataSource(dataSource);
          setcontrolLoaded(true);
        });
    }, 250);
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
        <button className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-primary k-grid-edit-command" onClick={() => { alert(props.dataItem[field].toString()); }}>{buttonDesc}</button>
      </td>
    );
  };
  // "organizationId": 7,
  // "name": "test",
  // "email": "test@email.com",
  // "website": "www.google.com",
  // "contact": "123456",
  // "organizationTypeId": 10,
  // "organizationSubTypeId": 12,
  // "department": null,
  // "departments": []
  var dashboardGrid = [];
  if (Array.isArray(dataSource) && dataSource.length) {
    dashboardGrid = (
      <Grid style={{ height: "600px" }}
        data={filterBy(dataSource, filter)} filterable={false} sortable={false} filter={filter} onFilterChange={(e) => { setFilter(e.filter); }}
        total={dataSource.length}      >
        <GridColumn field="name" title="Name" />
        <GridColumn field="email" width="250px" title="Email" />
        <GridColumn field="website" title="Website" />
        <GridColumn field="contact" width="250px" title="Contact" />
        <GridColumn cell={CustomCell} field="studentID" title=" " width="200px" />
      </Grid>
    );
  }
  return (
    <div>
      {!controlLoaded && <LoadingPanel />}
      <div style={{ marginTop: 75, marginRight: 20 }} className="col-md-12">
        <div className="page-head"><h6>{orgDesc}</h6>
          <ExcelExport data={dataSource} ref={_export} fileName="dashboardGrid.xlsx">
            {dashboardGrid}
          </ExcelExport>
          < GridPDFExport ref={(pdfExport) => (gridPDFExport = pdfExport)} margin="1cm" fileName="dashboardGrid.pdf">
            {dashboardGrid}
          </GridPDFExport>
        </div >
      </div>
    </div>
  );
} export default Dashboard;  
