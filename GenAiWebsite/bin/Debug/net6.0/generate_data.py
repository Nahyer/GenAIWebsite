
# import pandas as pd
# import matplotlib.pyplot as plt
# from io import BytesIO
# import base64

# # Load the CSV data
# csv_file = '~/py/salaries2023.csv'
# data = pd.read_csv(csv_file)

# # Generate HTML table
# table_html = data.to_html(classes='table table-bordered table-hover')

# # Generate a bar chart
# chart_data = data.groupby('job_title')['salary_in_usd'].mean()
# plt.figure(figsize=(10, 5))
# chart_data.plot(kind='bar')
# plt.title('Average Salary by Job Title')
# plt.xlabel('Job Title')
# plt.ylabel('Average Salary (USD)')
# plt.tight_layout()

# # Save the chart as a BytesIO object and convert to base64
# chart_buffer = BytesIO()
# plt.savefig(chart_buffer, format='png')
# chart_buffer.seek(0)
# chart_base64 = base64.b64encode(chart_buffer.read()).decode('utf-8')

# # Define a function to return HTML content
# def generate_data_visualizations():
#     return {
#         'table_html': table_html,
#         'chart_html': f'<img src="data:image/png;base64,{chart_base64}" alt="Average Salary Chart">'
#     }

# # Call the function to generate data visualizations
# if __name__ == "__main__":
#     result = generate_data_visualizations()
#     print(result)

# import pandas as pd
# import plotly.express as px
# from io import BytesIO
# import base64

# # Load the CSV data
# csv_file = '~\\C# PROJECTS\\genAiWebsite\\GenAiSite\\GenAiWebsite\\GenAiWebsite\\wwwroot\\csv\\salaries2023.csv'
# data = pd.read_csv(csv_file)

# # Generate HTML table
# table_html = data.to_html(classes='table table-bordered table-hover')

# # Generate a bar chart using Plotly Express
# fig = px.bar(data, x='job_title', y='salary_in_usd', title='Average Salary by Job Title', labels={'job_title': 'Job Title', 'salary_in_usd': 'Average Salary (USD)'})
# chart_bytes = fig.to_image(format='png')
# chart_base64 = base64.b64encode(chart_bytes).decode('utf-8')

# # Define a function to return HTML content
# def generate_data_visualizations():
#     return {
#         'table_html': table_html,
#         'chart_html': f'<img src="data:image/png;base64,{chart_base64}" alt="Average Salary Chart">'
#     }

# # Call the function to generate data visualizations
# if __name__ == "__main__":
#     result = generate_data_visualizations()
#     print(result)
import pandas as pd
import plotly.express as px
from io import BytesIO
import base64

# Load the CSV data
csv_file = '~\\C# PROJECTS\\genAiWebsite\\GenAiSite\\GenAiWebsite\\GenAiWebsite\\wwwroot\\csv\\salaries2023.csv'
data = pd.read_csv(csv_file)

# Filter the data for the year 2023
data_2023 = data[data['work_year'] == 2023]

# Sort the filtered data by 'salary' in descending order
sorted_data_2023 = data_2023.sort_values(by='salary', ascending=False)

# Select the top 5 highest paid employees for 2023
top_5_employees_2023 = sorted_data_2023.head(5)

# Generate a scrollable HTML table for the top 5 highest paid employees
table_html = f'<div style="width: 600px; overflow-x: auto;">{top_5_employees_2023.to_html(classes="table table-bordered table-hover")}</div>'

# Generate a bar chart for salaries in the last 4 years
last_4_years_data = data[data['work_year'].isin([2020, 2021, 2022, 2023])]
fig_bar = px.bar(
    last_4_years_data,
    x='work_year',
    y='salary_in_usd',
    title='Average Salary by Year (Last 4 Years)',
    labels={'salary_in_usd': 'Average Salary (USD)'},
)

# Generate a pie chart for job distribution among top 10 in-demand job titles
top_10_job_titles = data['job_title'].value_counts().head(10).index
job_distribution_data = data[data['job_title'].isin(top_10_job_titles)]
fig_pie = px.pie(
    job_distribution_data,
    names='job_title',
    title='Job Distribution Among Top 10 In-Demand Job Titles',
)

# Save the chart as BytesIO and convert to base64
chart_buffer = BytesIO()
fig_bar.write_image(chart_buffer, format='png')
chart_buffer.seek(0)
chart_base64 = base64.b64encode(chart_buffer.read()).decode('utf-8')

# Save pie chart as BytesIO and convert to base64 
pie_buffer = BytesIO()
fig_pie.write_image(pie_buffer, format='png')
pie_buffer.seek(0)  
pie_base64 = base64.b64encode(pie_buffer.read()).decode('utf-8')

# Define a function to return HTML content
def generate_data_visualizations():
    return {
        'table_html': table_html,
        'chart_html_bar': f'<img src="data:image/png;base64,{chart_base64}" alt="Average Salary Chart">',
        'chart_html_pie': f'<img src="data:image/png;base64,{pie_base64}">' 
    }

# Call the function to generate data visualizations
if __name__ == "__main__":
    result = generate_data_visualizations()
    print(result)
