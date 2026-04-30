import json
from datetime import datetime

class ReportGenerator:

    def generate_html(self, incidents):

        total = len(incidents)
        critical = len([i for i in incidents if i.severity == "critical"])
        high = len([i for i in incidents if i.severity == "high"])
        medium = len([i for i in incidents if i.severity == "medium"])
        low = len([i for i in incidents if i.severity == "low"])

        def severity_color(sev):
            return {
                "critical": "#ff4d4d",
                "high": "#ff944d",
                "medium": "#ffd11a",
                "low": "#66cc66"
            }.get(sev, "#ccc")

        html = f"""
        <html>
        <head>
            <title>IT Incident Report</title>
            <style>
                body {{
                    font-family: Arial;
                    background-color: #f5f7fa;
                    margin: 0;
                }}

                .header {{
                    background: #1f3c88;
                    color: white;
                    padding: 15px;
                    font-size: 20px;
                }}

                .container {{
                    padding: 20px;
                }}

                .card-container {{
                    display: flex;
                    gap: 15px;
                    margin-bottom: 20px;
                }}

                .card {{
                    background: white;
                    padding: 15px;
                    border-radius: 8px;
                    flex: 1;
                    text-align: center;
                    box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                }}

                table {{
                    width: 100%;
                    border-collapse: collapse;
                    background: white;
                }}

                th {{
                    background: #1f3c88;
                    color: white;
                    padding: 10px;
                }}

                td {{
                    padding: 10px;
                    border-bottom: 1px solid #ddd;
                }}

                .badge {{
                    padding: 5px 10px;
                    border-radius: 5px;
                    color: white;
                    font-size: 12px;
                }}
            </style>
        </head>

        <body>

        <div class="header">
            IT Incident Auto-Triage Report  
            <div style="font-size:12px;">
                Generated: {datetime.now().strftime("%Y-%m-%d %H:%M")}
            </div>
        </div>

        <div class="container">

            <h3>Summary</h3>
            <div class="card-container">
                <div class="card"><b>{total}</b><br>Total</div>
                <div class="card"><b style="color:red;">{critical}</b><br>Critical</div>
                <div class="card"><b style="color:orange;">{high}</b><br>High</div>
                <div class="card"><b style="color:gold;">{medium}</b><br>Medium</div>
                <div class="card"><b style="color:green;">{low}</b><br>Low</div>
            </div>

            <h3>Incident Details</h3>

            <table>
                <tr>
                    <th>ID</th>
                    <th>Title</th>
                    <th>Severity</th>
                    <th>Type</th>
                    <th>Team</th>
                    <th>Tickets</th>
                </tr>
        """

        for i in incidents:
            html += f"""
            <tr>
                <td>{i.id}</td>
                <td>{i.title}</td>
                <td>
                    <span class="badge" style="background:{severity_color(i.severity)}">
                        {i.severity.upper()}
                    </span>
                </td>
                <td>{i.type}</td>
                <td>{i.assigned_team}</td>
                <td>
                    JIRA: {i.ticket_ids.get("jira", "")}<br>
                    SNOW: {i.ticket_ids.get("snow", "")}<br>
                    AZURE: {i.ticket_ids.get("azure", "")}
                </td>
            </tr>
            """

        html += """
            </table>
        </div>
        </body>
        </html>
        """

        with open("output/report.html", "w") as f:
            f.write(html)

        print("HTML Report Generated!")

    def export_json(self, incidents):
        with open("output/report.json", "w") as f:
            json.dump([i.to_dict() for i in incidents], f, indent=2)