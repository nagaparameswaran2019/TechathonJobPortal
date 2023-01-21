import React from "react";
import { Link } from "react-router-dom";

const Footer = () => {
  return (
    <div className="nk-footer">
      <div className="container-fluid">
        <div className="nk-footer-wrap">
          <div className="nk-footer-copyright">
           
          </div>
          <div className="nk-footer-links">
            <ul className="nav nav-sm">
              <li className="nav-item">
                <Link  to=""  className="nav-link">
                  Terms
                </Link>
              </li>
              <li className="nav-item">
                <Link to="" className="nav-link">
                  Privacy
                </Link>
              </li>
              <li className="nav-item">
                <Link  to="" className="nav-link">
                  Help
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
};
export default Footer;
