import React, { Suspense, useLayoutEffect } from "react";
import { Switch, Route } from "react-router-dom";
const JobDetails = () => {
    useLayoutEffect(() => {
        window.scrollTo(0, 0);
    });

    return (
        <div style={{ marginTop: 65, position:"fixed" }}>
            TODO JobDetails
        </div>
    );
};
export default JobDetails;
