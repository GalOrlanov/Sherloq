import routesIcons from "../../assets/images/routes";
import DataAssets from "../DataAssets/DataAssets";
import DataReports from "../DataReports/Datareports";

export default [
  { title: "Data Report", path: "/reports", icon: routesIcons.dataReportIcon, component: DataReports },
  { title: "All Data Assets", path: "/dataAssets", icon: routesIcons.dataAssetsIcon, component: DataAssets },
];
