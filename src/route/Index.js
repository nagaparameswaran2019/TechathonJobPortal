import React, { Suspense, useLayoutEffect } from "react";
import { Switch, Route } from "react-router-dom";
import StudentDetails from "../pages/app/StudentDetails" 
import CollegeDashboard from "../pages/app/CollegeDashboard" 
import DepartmentDetails from "../pages/app/DepartmentDetails" 
import JobDetails from "../pages/app/JobDetails" 
 

const Pages = () => {
  useLayoutEffect(() => {
    window.scrollTo(0, 0);
  });

  return (
    <Suspense fallback={<div />}>
      <Switch> 
        <Route exact path={`${process.env.PUBLIC_URL}/pages/app/StudentDetails`}  component={StudentDetails}></Route> 
        {/* <Route exact path={`${process.env.PUBLIC_URL}/pages/app/CollegeDashboard`} component={CollegeDashboard}></Route>  */}
        <Route exact path={`${process.env.PUBLIC_URL}/pages/app/CollegeDashboard`} component={CollegeDashboard}></Route> 
        <Route exact path={`${process.env.PUBLIC_URL}/pages/app/DepartmentDetails`} component={DepartmentDetails}></Route> 
        <Route exact path={`${process.env.PUBLIC_URL}/pages/app/JobDetails`} component={JobDetails}></Route> 
      </Switch>
    </Suspense>
  );
};
export default Pages;
