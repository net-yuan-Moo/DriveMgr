USE [master]
GO
/****** Object:  Database [db_FinancialMgr]    Script Date: 05/03/2017 22:36:42 ******/
CREATE DATABASE [db_FinancialMgr] ON  PRIMARY 
( NAME = N'db_FinancialMgr', FILENAME = N'I:\项目\SmartOfLife\DB\db_FinancialMgr.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'db_FinancialMgr_log', FILENAME = N'I:\项目\SmartOfLife\DB\db_FinancialMgr_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [db_FinancialMgr] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_FinancialMgr].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_FinancialMgr] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [db_FinancialMgr] SET ANSI_NULLS OFF
GO
ALTER DATABASE [db_FinancialMgr] SET ANSI_PADDING OFF
GO
ALTER DATABASE [db_FinancialMgr] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [db_FinancialMgr] SET ARITHABORT OFF
GO
ALTER DATABASE [db_FinancialMgr] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [db_FinancialMgr] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [db_FinancialMgr] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [db_FinancialMgr] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [db_FinancialMgr] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [db_FinancialMgr] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [db_FinancialMgr] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [db_FinancialMgr] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [db_FinancialMgr] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [db_FinancialMgr] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [db_FinancialMgr] SET  DISABLE_BROKER
GO
ALTER DATABASE [db_FinancialMgr] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [db_FinancialMgr] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [db_FinancialMgr] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [db_FinancialMgr] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [db_FinancialMgr] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [db_FinancialMgr] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [db_FinancialMgr] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [db_FinancialMgr] SET  READ_WRITE
GO
ALTER DATABASE [db_FinancialMgr] SET RECOVERY SIMPLE
GO
ALTER DATABASE [db_FinancialMgr] SET  MULTI_USER
GO
ALTER DATABASE [db_FinancialMgr] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [db_FinancialMgr] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'db_FinancialMgr', N'ON'
GO
USE [db_FinancialMgr]
GO
/****** Object:  Table [dbo].[tb_EnterStoreHouse]    Script Date: 05/03/2017 22:36:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_EnterStoreHouse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnterSN] [varchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[HandlePerson] [varchar](50) NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_EnterStoreHouse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入库单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_EnterStoreHouse', @level2type=N'COLUMN',@level2name=N'EnterSN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入库时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_EnterStoreHouse', @level2type=N'COLUMN',@level2name=N'EnterDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'办理人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_EnterStoreHouse', @level2type=N'COLUMN',@level2name=N'HandlePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_EnterStoreHouse', @level2type=N'COLUMN',@level2name=N'Remark'
GO
/****** Object:  Table [dbo].[tb_BusinessEntertain]    Script Date: 05/03/2017 22:36:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_BusinessEntertain](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntertainAmount] [money] NULL,
	[EntertainObject] [varchar](100) NULL,
	[Transactor] [varchar](50) NULL,
	[TransactDate] [datetime] NULL,
	[EntertainUse] [varchar](100) NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_BusinessEntertain] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'招待费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'EntertainAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'招待对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'EntertainObject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经办人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'Transactor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'招待日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'TransactDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用途' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'EntertainUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务招待管理表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_BusinessEntertain'
GO
/****** Object:  StoredProcedure [dbo].[sp_Pager]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Pager]
@tableName varchar(64),  --分页表名
@columns varchar(512),  --查询的字段
@order varchar(256),    --排序方式
@pageSize int,  --每页大小
@pageIndex int,  --当前页
@where varchar(1024) = '1=1',  --查询条件
@totalCount int output  --总记录数
as
declare @beginIndex int,@endIndex int,@sqlResult nvarchar(2000),@sqlGetCount nvarchar(2000)
set @beginIndex = (@pageIndex - 1) * @pageSize + 1  --开始
set @endIndex = (@pageIndex) * @pageSize  --结束
set @sqlresult = 'select '+@columns+' from (
select row_number() over(order by '+ @order +')
as Rownum,'+@columns+'
from '+@tableName+' where '+ @where +') as T
where T.Rownum between ' + CONVERT(varchar(max),@beginIndex) + ' and ' + CONVERT(varchar(max),@endIndex)
set @sqlGetCount = 'select @totalCount = count(*) from '+@tablename+' where ' + @where  --总数
--print @sqlresult
exec(@sqlresult)
exec sp_executesql @sqlGetCount,N'@totalCount int output',@totalCount output
--测试调用：
--declare @total int
--exec sp_Pager 'tbLoginInfo','Id,UserName,Success','LoginDate','desc',4,2,'1=1',@total output
--print @total
GO
/****** Object:  Table [dbo].[tb_ExpensesCategory]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_ExpensesCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NULL,
	[BelongDataBase] [varchar](50) NULL,
	[BelongTable] [varchar](50) NULL,
	[BelongMember] [varchar](50) NULL,
	[BelongTimeMember] [varchar](50) NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_ExpensesCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory', @level2type=N'COLUMN',@level2name=N'CategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支出分类表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ExpensesCategory'
GO
/****** Object:  Table [dbo].[tb_ExamPay]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_ExamPay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentsID] [bigint] NULL,
	[RealPay] [money] NULL,
	[Remark] [varchar](500) NULL,
	[Operater] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
	[PayTime] [datetime] NULL,
 CONSTRAINT [PK_tb_ExamPay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Repayment]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Repayment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RepaymentAmount] [money] NULL,
	[Bank] [varchar](50) NULL,
	[RepaymentPerson] [varchar](50) NULL,
	[RepaymentDate] [datetime] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_Repayment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还款金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'RepaymentAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'银行' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还款人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'RepaymentPerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还款日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'RepaymentDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还款管理表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Repayment'
GO
/****** Object:  Table [dbo].[tb_PriceConfig]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_PriceConfig](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[PriceTypeName] [varchar](50) NULL,
	[ConfigType] [int] NULL,
	[Price] [money] NULL,
	[Remark] [varchar](500) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_PriceConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单价类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_PriceConfig', @level2type=N'COLUMN',@level2name=N'PriceTypeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_PriceConfig', @level2type=N'COLUMN',@level2name=N'ConfigType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_PriceConfig', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_PriceConfig', @level2type=N'COLUMN',@level2name=N'Remark'
GO
/****** Object:  Table [dbo].[tb_OutStoreHouse]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_OutStoreHouse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OutSN] [varchar](50) NULL,
	[OutDate] [datetime] NULL,
	[HandlePerson] [varchar](50) NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_OutStoreHouse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出库单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_OutStoreHouse', @level2type=N'COLUMN',@level2name=N'OutSN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出库时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_OutStoreHouse', @level2type=N'COLUMN',@level2name=N'OutDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'办理人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_OutStoreHouse', @level2type=N'COLUMN',@level2name=N'HandlePerson'
GO
/****** Object:  Table [dbo].[tb_Office]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Office](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OfficeUse] [varchar](100) NULL,
	[TagPerson] [varchar](50) NULL,
	[UseDate] [datetime] NULL,
	[OfficeAmount] [money] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_Office] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用途' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'OfficeUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'责任人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'TagPerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'办公费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'OfficeAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'办公费用管理表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Office'
GO
/****** Object:  Table [dbo].[tb_Loan]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Loan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoanAmount] [money] NULL,
	[Bank] [varchar](50) NULL,
	[Lenders] [varchar](50) NULL,
	[LoanDate] [datetime] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_Loan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'贷款金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'LoanAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'银行' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'贷款人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'Lenders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'贷款日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'LoanDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'贷款管理表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Loan'
GO
/****** Object:  Table [dbo].[tb_IncomeCategory]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_IncomeCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NULL,
	[BelongDataBase] [varchar](50) NULL,
	[BelongTable] [varchar](50) NULL,
	[BelongMember] [varchar](50) NULL,
	[BelongTimeMember] [varchar](50) NULL,
	[Remark] [varchar](100) NULL,
	[DeleteMark] [bit] NULL,
	[CreatePerson] [varchar](15) NULL,
	[CreateTime] [datetime] NULL,
	[UpdatePerson] [varchar](15) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_tb_IncomeCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收入分类表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_IncomeCategory'
GO
/****** Object:  Table [dbo].[tb_GoodsCategory]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_GoodsCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_GoodsCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_GoodsCategory', @level2type=N'COLUMN',@level2name=N'CategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_GoodsCategory', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_GoodsCategory', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_GoodsCategory', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_GoodsCategory', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_GoodsCategory', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_GoodsCategory', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
/****** Object:  Table [dbo].[tb_Vehicle]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LicencePlateNum] [varchar](50) NULL,
	[Brands] [varchar](50) NULL,
	[CarModel] [varchar](50) NULL,
	[BuyPrice] [money] NULL,
	[BuyDate] [datetime] NULL,
	[Owner] [varchar](50) NULL,
	[Status] [int] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_TB_VEHICLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车牌号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'LicencePlateNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'Brands'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'型号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'CarModel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'BuyDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所有者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'Owner'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车辆管理表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Vehicle'
GO
/****** Object:  Table [dbo].[tb_Tuition]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Tuition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Costs] [money] NULL,
	[LocalType] [int] NULL,
	[Remark] [varchar](500) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_TB_TUITION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition', @level2type=N'COLUMN',@level2name=N'Costs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition', @level2type=N'COLUMN',@level2name=N'LocalType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录学费情况。只有一条学费记录。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Tuition'
GO
/****** Object:  Table [dbo].[tb_Travel]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Travel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TravelAmount] [money] NULL,
	[TravelPerson] [varchar](50) NULL,
	[TravelUse] [varchar](100) NULL,
	[TraveDate] [datetime] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_Travel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'TravelAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出差人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'TravelPerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用途' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'TravelUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'差旅管理表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Travel'
GO
/****** Object:  Table [dbo].[tb_SiteRental_local]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_SiteRental_local](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[StudentsID] [int] NOT NULL,
	[PriceConfigID] [int] NULL,
	[Longer] [float] NULL,
	[TotalPrice] [money] NULL,
	[RentDate] [datetime] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_TB_SITERENTAL_local] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'场地出租情况记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental_local'
GO
/****** Object:  Table [dbo].[tb_SiteRental]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_SiteRental](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[RentObject] [varchar](100) NULL,
	[PriceConfigID] [int] NULL,
	[Longer] [float] NULL,
	[TotalPrice] [money] NULL,
	[RentDate] [datetime] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_TB_SITERENTAL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description_local', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车辆外键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'VehicleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description_local', @value=N'车辆外键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'VehicleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出租对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'RentObject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'Longer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description_local', @value=N'时长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'Longer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description_local', @value=N'总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出租时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'RentDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description_local', @value=N'出租时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'RentDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description_local', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description_local', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'场地出租情况记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_SiteRental'
GO
/****** Object:  Table [dbo].[tb_VehiclMaintenance]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_VehiclMaintenance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[MaintenanceType] [int] NULL,
	[MaintenCosts] [money] NULL,
	[MaintenPerson] [varchar](50) NULL,
	[MaintenDate] [datetime] NULL,
	[Remark] [varchar](100) NOT NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_TB_VEHICLMAINTENANCE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'维护类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'MaintenanceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'MaintenCosts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'维护人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'MaintenPerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'维护时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'MaintenDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车辆维护情况记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehiclMaintenance'
GO
/****** Object:  Table [dbo].[tb_VehicleRental_local]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_VehicleRental_local](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[PriceConfigID] [int] NULL,
	[Longer] [float] NULL,
	[TotalPrice] [money] NULL,
	[StudentsID] [int] NULL,
	[RentDate] [datetime] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_TB_VEHICLERENTAL_local] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_VehicleRental]    Script Date: 05/03/2017 22:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_VehicleRental](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[PriceConfigID] [int] NULL,
	[Longer] [float] NULL,
	[TotalPrice] [money] NULL,
	[StudentName] [varchar](50) NULL,
	[StudentCode] [varchar](50) NULL,
	[RentDate] [datetime] NULL,
	[Remark] [varchar](100) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_TB_VEHICLERENTAL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车辆编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'VehicleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'Longer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'维护人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'StudentName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出租对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'RentDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'CreatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'UpdatePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental', @level2type=N'COLUMN',@level2name=N'DeleteMark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车辆出租情况记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_VehicleRental'
GO
/****** Object:  View [dbo].[V_PriceConfig]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_PriceConfig]
AS
SELECT     Id, PriceTypeName, Price, Remark, CreatePerson, CreateDate, UpdatePerson, UpdateDate, DeleteMark, ConfigType, 
                      CASE ConfigType WHEN 0 THEN '车辆' WHEN 1 THEN '场地' ELSE '' END AS ConfigTypeName
FROM         dbo.tb_PriceConfig
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[17] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tb_PriceConfig"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 201
            End
            DisplayFlags = 280
            TopColumn = 6
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_PriceConfig'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_PriceConfig'
GO
/****** Object:  Table [dbo].[tb_Goods]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Goods](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsName] [varchar](50) NULL,
	[GoodsCategoryID] [int] NULL,
	[MinQuantity] [int] NULL,
	[MaxQuantity] [int] NULL,
	[RealQuantity] [int] NULL,
	[Specification] [varchar](50) NULL,
	[HandlePerson] [varchar](50) NULL,
	[Remark] [varchar](50) NULL,
	[CreatePerson] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdatePerson] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_Goods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Goods', @level2type=N'COLUMN',@level2name=N'GoodsName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物资编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Goods', @level2type=N'COLUMN',@level2name=N'GoodsCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最小数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Goods', @level2type=N'COLUMN',@level2name=N'MinQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Goods', @level2type=N'COLUMN',@level2name=N'MaxQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Goods', @level2type=N'COLUMN',@level2name=N'RealQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Goods', @level2type=N'COLUMN',@level2name=N'Specification'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'办理人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Goods', @level2type=N'COLUMN',@level2name=N'HandlePerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Goods', @level2type=N'COLUMN',@level2name=N'Remark'
GO
/****** Object:  Table [dbo].[tb_OutStoreHouseDetails]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_OutStoreHouseDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OutDetailsSN] [varchar](50) NULL,
	[GoodsID] [int] NULL,
	[OutStoreHouseID] [int] NULL,
	[OutQuantity] [int] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_OutStoreHouseDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出库单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_OutStoreHouseDetails', @level2type=N'COLUMN',@level2name=N'OutDetailsSN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_OutStoreHouseDetails', @level2type=N'COLUMN',@level2name=N'GoodsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出库编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_OutStoreHouseDetails', @level2type=N'COLUMN',@level2name=N'OutStoreHouseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出库数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_OutStoreHouseDetails', @level2type=N'COLUMN',@level2name=N'OutQuantity'
GO
/****** Object:  Table [dbo].[tb_EnterStoreHouseDetails]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_EnterStoreHouseDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnterDetailsSN] [varchar](50) NULL,
	[GoodsID] [int] NULL,
	[EnterQuantity] [int] NULL,
	[EnterStoreHouseID] [int] NULL,
	[DeleteMark] [bit] NULL,
 CONSTRAINT [PK_tb_EnterStoreHouseDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_EnterStoreHouseDetails', @level2type=N'COLUMN',@level2name=N'EnterDetailsSN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_EnterStoreHouseDetails', @level2type=N'COLUMN',@level2name=N'GoodsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_EnterStoreHouseDetails', @level2type=N'COLUMN',@level2name=N'EnterQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入库编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_EnterStoreHouseDetails', @level2type=N'COLUMN',@level2name=N'EnterStoreHouseID'
GO
/****** Object:  StoredProcedure [dbo].[up_incomeCategary]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_incomeCategary]
	-- Add the parameters for the stored procedure here
	@year varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
-------------查询一年的贷款----------------------
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Loan.LoanAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Loan where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Loan.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Loan.CreateDate) = 12
     ) select * from cte order by Paymonth
                           
-----查询一年的学费            
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(SmartOfLifeDriveMgr.dbo.tb_Pay.RealPay),0) SumPay
     FROM SmartOfLifeDriveMgr.dbo.tb_Pay where DATEPART(YEAR,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime)=@year
     and DATEPART(MONTH,SmartOfLifeDriveMgr.dbo.tb_Pay.PayTime) = 12
     ) select * from cte order by Paymonth
     
     -----查询一年的考试费            
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_ExamPay.RealPay),0) SumPay
     FROM db_FinancialMgr.dbo.tb_ExamPay where DATEPART(YEAR,db_FinancialMgr.dbo.tb_ExamPay.PayTime)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_ExamPay.PayTime) = 12
     ) select * from cte order by Paymonth
                           
                           
------查询一年的包车费用--------------------------                 

;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental.CreateDate) = 12
     ) select * from cte order by Paymonth
     
     ------查询一年的包车费用(本校学员)--------------------------                 

;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehicleRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehicleRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehicleRental_local.CreateDate) = 12
     ) select * from cte order by Paymonth
     
----查询一年的场地费用----------------------     
                           
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental.CreateDate) = 12
     ) select * from cte order by Paymonth
     
     ----查询一年的场地费用(本地)----------------------     
                           
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_SiteRental_local.TotalPrice),0) SumPay
     FROM db_FinancialMgr.dbo.tb_SiteRental_local where DATEPART(YEAR,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_SiteRental_local.CreateDate) = 12
     ) select * from cte order by Paymonth
     
END
GO
/****** Object:  StoredProcedure [dbo].[up_expensesCategory]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_expensesCategory]
	-- Add the parameters for the stored procedure here
	@year varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
----查询一年的还款---------------------------------------
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Repayment.RepaymentAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Repayment where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Repayment.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Repayment.CreateDate) = 12
     ) select * from cte order by Paymonth
                           
--查询一年的车辆维护费用------------------             
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_VehiclMaintenance.MaintenCosts),0) SumPay
     FROM db_FinancialMgr.dbo.tb_VehiclMaintenance where DATEPART(YEAR,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_VehiclMaintenance.CreateDate) = 12
     ) select * from cte order by Paymonth
                           
------查询一年的业务招待费-------------------                   
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_BusinessEntertain.EntertainAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_BusinessEntertain where DATEPART(YEAR,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_BusinessEntertain.CreateDate) = 12
     ) select * from cte order by Paymonth
     
-------查询一年差旅费用-----------------------------------
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Travel.TravelAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Travel where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Travel.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Travel.CreateDate) = 12
     ) select * from cte order by Paymonth
     
---查询一年的办工费用
;with cte as
(
     SELECT 1 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 1
     union
     SELECT 2 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 2
     union
     SELECT 3 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 3
     union
     SELECT 4 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 4
     union
     SELECT 5 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 5
     union
     SELECT 6 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 6
     union
     SELECT 7 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 7
     union
     SELECT 8 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 8
     union
     SELECT 9 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 9
     union
     SELECT 10 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 10
     union
     SELECT 11 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 11
     union
     SELECT 12 Paymonth,ISNULL(sum(db_FinancialMgr.dbo.tb_Office.OfficeAmount),0) SumPay
     FROM db_FinancialMgr.dbo.tb_Office where DATEPART(YEAR,db_FinancialMgr.dbo.tb_Office.CreateDate)=@year
     and DATEPART(MONTH,db_FinancialMgr.dbo.tb_Office.CreateDate) = 12
     ) select * from cte order by Paymonth
END
GO
/****** Object:  View [dbo].[V_Goods]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Goods]
AS
SELECT     A.Id, A.GoodsName, A.GoodsCategoryID, A.MinQuantity, A.MaxQuantity, A.RealQuantity, A.Specification, A.HandlePerson, A.Remark, A.CreatePerson, A.CreateDate, A.UpdatePerson, A.UpdateDate, 
                      A.DeleteMark, B.CategoryName
FROM         dbo.tb_Goods AS A LEFT OUTER JOIN
                      dbo.tb_GoodsCategory AS B ON A.GoodsCategoryID = B.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 213
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 251
               Bottom = 125
               Right = 412
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Goods'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Goods'
GO
/****** Object:  View [dbo].[V_VehiclMaintenance]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_VehiclMaintenance]
AS
SELECT     A.Id, A.VehicleId, A.MaintenCosts, A.MaintenDate, A.MaintenPerson, A.Remark, A.CreateDate, A.CreatePerson, A.UpdateDate, A.UpdatePerson, A.DeleteMark, A.MaintenanceType, 
                      CASE A.MaintenanceType WHEN 0 THEN '加油费' WHEN 1 THEN '加气费' WHEN 2 THEN '修理费' WHEN 3 THEN '过路费' WHEN 4 THEN '停车费' WHEN 5 THEN '其他' ELSE '' END AS MaintenanceTypeName,
                       B.LicencePlateNum
FROM         dbo.tb_VehiclMaintenance AS A LEFT OUTER JOIN
                      dbo.tb_Vehicle AS B ON A.VehicleId = B.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 250
               Bottom = 125
               Right = 419
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_VehiclMaintenance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_VehiclMaintenance'
GO
/****** Object:  View [dbo].[V_VehicleRental]    Script Date: 05/03/2017 22:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_VehicleRental]
AS
SELECT     A.Id, A.VehicleId, A.PriceConfigID, A.Longer, A.TotalPrice, A.StudentName, A.StudentCode, A.RentDate, A.Remark, A.CreatePerson, A.CreateDate, A.UpdatePerson, 
                      A.UpdateDate, A.DeleteMark, B.PriceTypeName, B.Price, C.LicencePlateNum
FROM         dbo.tb_VehicleRental AS A LEFT OUTER JOIN
                      dbo.tb_PriceConfig AS B ON A.PriceConfigID = B.Id LEFT OUTER JOIN
                      dbo.tb_Vehicle AS C ON A.VehicleId = C.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 195
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 233
               Bottom = 125
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 434
               Bottom = 125
               Right = 603
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_VehicleRental'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_VehicleRental'
GO
/****** Object:  View [dbo].[V_SiteRental]    Script Date: 05/03/2017 22:36:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_SiteRental]
AS
SELECT     A.Id, A.VehicleId, A.PriceConfigID, A.RentDate, A.RentObject, A.Longer, A.TotalPrice, A.Remark, A.CreatePerson, A.CreateDate, A.UpdatePerson, A.UpdateDate, A.DeleteMark, B.PriceTypeName, 
                      B.Price, C.LicencePlateNum
FROM         dbo.tb_SiteRental AS A LEFT OUTER JOIN
                      dbo.tb_PriceConfig AS B ON A.PriceConfigID = B.Id LEFT OUTER JOIN
                      dbo.tb_Vehicle AS C ON A.VehicleId = C.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 195
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 233
               Bottom = 125
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 434
               Bottom = 125
               Right = 603
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_SiteRental'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_SiteRental'
GO
/****** Object:  View [dbo].[V_OutStoreHouseDetails]    Script Date: 05/03/2017 22:36:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_OutStoreHouseDetails]
AS
SELECT     A.Id, A.OutDetailsSN, A.GoodsID, A.OutQuantity, A.OutStoreHouseID, A.DeleteMark, B.OutSN, C.GoodsName, C.GoodsCategoryID, D.CategoryName
FROM         dbo.tb_OutStoreHouseDetails AS A LEFT OUTER JOIN
                      dbo.tb_OutStoreHouse AS B ON A.OutStoreHouseID = B.Id LEFT OUTER JOIN
                      dbo.tb_Goods AS C ON A.GoodsID = C.Id LEFT OUTER JOIN
                      dbo.tb_GoodsCategory AS D ON C.GoodsCategoryID = D.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 250
               Bottom = 125
               Right = 407
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 445
               Bottom = 125
               Right = 620
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "D"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_OutStoreHouseDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_OutStoreHouseDetails'
GO
/****** Object:  View [dbo].[V_EnterStoreHouseDetails]    Script Date: 05/03/2017 22:36:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_EnterStoreHouseDetails]
AS
SELECT     A.Id, A.EnterDetailsSN, A.GoodsID, A.EnterQuantity, A.EnterStoreHouseID, A.DeleteMark, B.EnterSN, C.GoodsName, C.GoodsCategoryID, D.CategoryName
FROM         dbo.tb_EnterStoreHouseDetails AS A LEFT OUTER JOIN
                      dbo.tb_EnterStoreHouse AS B ON A.EnterStoreHouseID = B.Id LEFT OUTER JOIN
                      dbo.tb_Goods AS C ON A.GoodsID = C.Id LEFT OUTER JOIN
                      dbo.tb_GoodsCategory AS D ON C.GoodsCategoryID = D.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 258
               Bottom = 125
               Right = 415
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 453
               Bottom = 125
               Right = 628
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "D"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_EnterStoreHouseDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_EnterStoreHouseDetails'
GO
/****** Object:  Default [DF_tb_BusinessEntertain_CreateDate]    Script Date: 05/03/2017 22:36:44 ******/
ALTER TABLE [dbo].[tb_BusinessEntertain] ADD  CONSTRAINT [DF_tb_BusinessEntertain_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_BusinessEntertain_UpdateDate]    Script Date: 05/03/2017 22:36:44 ******/
ALTER TABLE [dbo].[tb_BusinessEntertain] ADD  CONSTRAINT [DF_tb_BusinessEntertain_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_BusinessEntertain_DeleteMark]    Script Date: 05/03/2017 22:36:44 ******/
ALTER TABLE [dbo].[tb_BusinessEntertain] ADD  CONSTRAINT [DF_tb_BusinessEntertain_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_ExpensesCategory_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_ExpensesCategory] ADD  CONSTRAINT [DF_tb_ExpensesCategory_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_ExamPay_Costs]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_ExamPay] ADD  CONSTRAINT [DF_tb_ExamPay_Costs]  DEFAULT ((0)) FOR [RealPay]
GO
/****** Object:  Default [DF_tb_ExamPay_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_ExamPay] ADD  CONSTRAINT [DF_tb_ExamPay_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_ExamPay_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_ExamPay] ADD  CONSTRAINT [DF_tb_ExamPay_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_ExamPay_PayTime]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_ExamPay] ADD  CONSTRAINT [DF_tb_ExamPay_PayTime]  DEFAULT (getdate()) FOR [PayTime]
GO
/****** Object:  Default [DF_tb_Repayment_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Repayment] ADD  CONSTRAINT [DF_tb_Repayment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_Repayment_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Repayment] ADD  CONSTRAINT [DF_tb_Repayment_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_Repayment_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Repayment] ADD  CONSTRAINT [DF_tb_Repayment_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_PriceConfig_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_PriceConfig] ADD  CONSTRAINT [DF_tb_PriceConfig_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_Office_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Office] ADD  CONSTRAINT [DF_tb_Office_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_Office_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Office] ADD  CONSTRAINT [DF_tb_Office_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_Loan_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Loan] ADD  CONSTRAINT [DF_tb_Loan_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_Loan_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Loan] ADD  CONSTRAINT [DF_tb_Loan_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_Loan_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Loan] ADD  CONSTRAINT [DF_tb_Loan_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_IncomeCategory_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_IncomeCategory] ADD  CONSTRAINT [DF_tb_IncomeCategory_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_Vehicle_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Vehicle] ADD  CONSTRAINT [DF_tb_Vehicle_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_Tuition_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Tuition] ADD  CONSTRAINT [DF_tb_Tuition_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_Tuition_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Tuition] ADD  CONSTRAINT [DF_tb_Tuition_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_Travel_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Travel] ADD  CONSTRAINT [DF_tb_Travel_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_Travel_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Travel] ADD  CONSTRAINT [DF_tb_Travel_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_Travel_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_Travel] ADD  CONSTRAINT [DF_tb_Travel_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_SiteRental_local_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental_local] ADD  CONSTRAINT [DF_tb_SiteRental_local_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_SiteRental_local_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental_local] ADD  CONSTRAINT [DF_tb_SiteRental_local_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_SiteRental_local_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental_local] ADD  CONSTRAINT [DF_tb_SiteRental_local_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_SiteRental_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental] ADD  CONSTRAINT [DF_tb_SiteRental_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_SiteRental_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental] ADD  CONSTRAINT [DF_tb_SiteRental_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_SiteRental_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental] ADD  CONSTRAINT [DF_tb_SiteRental_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_VehiclMaintenance_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehiclMaintenance] ADD  CONSTRAINT [DF_tb_VehiclMaintenance_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_VehiclMaintenance_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehiclMaintenance] ADD  CONSTRAINT [DF_tb_VehiclMaintenance_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_VehiclMaintenance_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehiclMaintenance] ADD  CONSTRAINT [DF_tb_VehiclMaintenance_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  Default [DF_tb_VehicleRental_CreateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehicleRental] ADD  CONSTRAINT [DF_tb_VehicleRental_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_tb_VehicleRental_UpdateDate]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehicleRental] ADD  CONSTRAINT [DF_tb_VehicleRental_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF_tb_VehicleRental_DeleteMark]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehicleRental] ADD  CONSTRAINT [DF_tb_VehicleRental_DeleteMark]  DEFAULT ((0)) FOR [DeleteMark]
GO
/****** Object:  ForeignKey [FK_tb_SiteRental_local_tb_PriceConfig]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental_local]  WITH CHECK ADD  CONSTRAINT [FK_tb_SiteRental_local_tb_PriceConfig] FOREIGN KEY([PriceConfigID])
REFERENCES [dbo].[tb_PriceConfig] ([Id])
GO
ALTER TABLE [dbo].[tb_SiteRental_local] CHECK CONSTRAINT [FK_tb_SiteRental_local_tb_PriceConfig]
GO
/****** Object:  ForeignKey [FK_tb_SiteRental_local_tb_Vehicle]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental_local]  WITH CHECK ADD  CONSTRAINT [FK_tb_SiteRental_local_tb_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[tb_Vehicle] ([Id])
GO
ALTER TABLE [dbo].[tb_SiteRental_local] CHECK CONSTRAINT [FK_tb_SiteRental_local_tb_Vehicle]
GO
/****** Object:  ForeignKey [FK_tb_SiteRental_tb_PriceConfig]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental]  WITH CHECK ADD  CONSTRAINT [FK_tb_SiteRental_tb_PriceConfig] FOREIGN KEY([PriceConfigID])
REFERENCES [dbo].[tb_PriceConfig] ([Id])
GO
ALTER TABLE [dbo].[tb_SiteRental] CHECK CONSTRAINT [FK_tb_SiteRental_tb_PriceConfig]
GO
/****** Object:  ForeignKey [FK_tb_SiteRental_tb_Vehicle]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_SiteRental]  WITH CHECK ADD  CONSTRAINT [FK_tb_SiteRental_tb_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[tb_Vehicle] ([Id])
GO
ALTER TABLE [dbo].[tb_SiteRental] CHECK CONSTRAINT [FK_tb_SiteRental_tb_Vehicle]
GO
/****** Object:  ForeignKey [FK_tb_VehiclMaintenance_tb_Vehicle]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehiclMaintenance]  WITH CHECK ADD  CONSTRAINT [FK_tb_VehiclMaintenance_tb_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[tb_Vehicle] ([Id])
GO
ALTER TABLE [dbo].[tb_VehiclMaintenance] CHECK CONSTRAINT [FK_tb_VehiclMaintenance_tb_Vehicle]
GO
/****** Object:  ForeignKey [FK_tb_VehicleRental_local_tb_PriceConfig]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehicleRental_local]  WITH CHECK ADD  CONSTRAINT [FK_tb_VehicleRental_local_tb_PriceConfig] FOREIGN KEY([PriceConfigID])
REFERENCES [dbo].[tb_PriceConfig] ([Id])
GO
ALTER TABLE [dbo].[tb_VehicleRental_local] CHECK CONSTRAINT [FK_tb_VehicleRental_local_tb_PriceConfig]
GO
/****** Object:  ForeignKey [FK_tb_VehicleRental_local_tb_Vehicle]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehicleRental_local]  WITH CHECK ADD  CONSTRAINT [FK_tb_VehicleRental_local_tb_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[tb_Vehicle] ([Id])
GO
ALTER TABLE [dbo].[tb_VehicleRental_local] CHECK CONSTRAINT [FK_tb_VehicleRental_local_tb_Vehicle]
GO
/****** Object:  ForeignKey [FK_tb_VehicleRental_tb_PriceConfig]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehicleRental]  WITH CHECK ADD  CONSTRAINT [FK_tb_VehicleRental_tb_PriceConfig] FOREIGN KEY([PriceConfigID])
REFERENCES [dbo].[tb_PriceConfig] ([Id])
GO
ALTER TABLE [dbo].[tb_VehicleRental] CHECK CONSTRAINT [FK_tb_VehicleRental_tb_PriceConfig]
GO
/****** Object:  ForeignKey [FK_tb_VehicleRental_tb_Vehicle]    Script Date: 05/03/2017 22:36:45 ******/
ALTER TABLE [dbo].[tb_VehicleRental]  WITH CHECK ADD  CONSTRAINT [FK_tb_VehicleRental_tb_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[tb_Vehicle] ([Id])
GO
ALTER TABLE [dbo].[tb_VehicleRental] CHECK CONSTRAINT [FK_tb_VehicleRental_tb_Vehicle]
GO
/****** Object:  ForeignKey [FK_tb_Goods_tb_GoodsCategory]    Script Date: 05/03/2017 22:36:46 ******/
ALTER TABLE [dbo].[tb_Goods]  WITH CHECK ADD  CONSTRAINT [FK_tb_Goods_tb_GoodsCategory] FOREIGN KEY([GoodsCategoryID])
REFERENCES [dbo].[tb_GoodsCategory] ([Id])
GO
ALTER TABLE [dbo].[tb_Goods] CHECK CONSTRAINT [FK_tb_Goods_tb_GoodsCategory]
GO
/****** Object:  ForeignKey [FK_tb_OutStoreHouseDetails_tb_Goods]    Script Date: 05/03/2017 22:36:46 ******/
ALTER TABLE [dbo].[tb_OutStoreHouseDetails]  WITH CHECK ADD  CONSTRAINT [FK_tb_OutStoreHouseDetails_tb_Goods] FOREIGN KEY([GoodsID])
REFERENCES [dbo].[tb_Goods] ([Id])
GO
ALTER TABLE [dbo].[tb_OutStoreHouseDetails] CHECK CONSTRAINT [FK_tb_OutStoreHouseDetails_tb_Goods]
GO
/****** Object:  ForeignKey [FK_tb_OutStoreHouseDetails_tb_OutStoreHouse]    Script Date: 05/03/2017 22:36:46 ******/
ALTER TABLE [dbo].[tb_OutStoreHouseDetails]  WITH CHECK ADD  CONSTRAINT [FK_tb_OutStoreHouseDetails_tb_OutStoreHouse] FOREIGN KEY([OutStoreHouseID])
REFERENCES [dbo].[tb_OutStoreHouse] ([Id])
GO
ALTER TABLE [dbo].[tb_OutStoreHouseDetails] CHECK CONSTRAINT [FK_tb_OutStoreHouseDetails_tb_OutStoreHouse]
GO
/****** Object:  ForeignKey [FK_tb_EnterStoreHouseDetails_tb_EnterStoreHouse]    Script Date: 05/03/2017 22:36:46 ******/
ALTER TABLE [dbo].[tb_EnterStoreHouseDetails]  WITH CHECK ADD  CONSTRAINT [FK_tb_EnterStoreHouseDetails_tb_EnterStoreHouse] FOREIGN KEY([EnterStoreHouseID])
REFERENCES [dbo].[tb_EnterStoreHouse] ([Id])
GO
ALTER TABLE [dbo].[tb_EnterStoreHouseDetails] CHECK CONSTRAINT [FK_tb_EnterStoreHouseDetails_tb_EnterStoreHouse]
GO
/****** Object:  ForeignKey [FK_tb_EnterStoreHouseDetails_tb_Goods]    Script Date: 05/03/2017 22:36:46 ******/
ALTER TABLE [dbo].[tb_EnterStoreHouseDetails]  WITH CHECK ADD  CONSTRAINT [FK_tb_EnterStoreHouseDetails_tb_Goods] FOREIGN KEY([GoodsID])
REFERENCES [dbo].[tb_Goods] ([Id])
GO
ALTER TABLE [dbo].[tb_EnterStoreHouseDetails] CHECK CONSTRAINT [FK_tb_EnterStoreHouseDetails_tb_Goods]
GO
