import { DataReportsContainer } from "./DataReports.styled";
import Accordion from "@/components/Accordion/Accordion";
import reportIcons from "@/assets/images/reports";
import { MOST_IN_USE_TABLES_TITLE, MOST_UNUSED_TABLES, TOP_INSIGHT_TITLE, UNUSED_TABLE_TITLE } from "@/utils/consts/dataReports";
import TopInsight from "./TopInsight/TopInsight";
const DataReports = () => {
  return (
    <DataReportsContainer>
      <Accordion component={<TopInsight />} icon={reportIcons.star} title={TOP_INSIGHT_TITLE} />
      <Accordion icon={reportIcons.x} title={UNUSED_TABLE_TITLE} />
      <Accordion icon={reportIcons.thumbDown} title={MOST_UNUSED_TABLES} />
      <Accordion icon={reportIcons.thumbUp} title={MOST_IN_USE_TABLES_TITLE} />
    </DataReportsContainer>
  );
};

export default DataReports;
